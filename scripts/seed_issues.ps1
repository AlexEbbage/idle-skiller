param(
    [string]$Backlog = "docs/backlog.yml",
    [switch]$DryRun,
    [string]$DefaultAssignee,
    [string]$Milestone,
    [switch]$NoLabelSeeding
)

function Require-Cmd([string]$name, [string]$installHint) {
    if (-not (Get-Command $name -ErrorAction SilentlyContinue)) {
        throw "$name not found. $installHint"
    }
}

function Get-RepoSlug {
    $info = gh repo view --json nameWithOwner 2>$null | ConvertFrom-Json
    if (-not $info) { throw "Not inside a Git repo linked to GitHub (gh repo view failed)." }
    return $info.nameWithOwner
}

function Read-BacklogFeatures([string]$path) {
    if (-not (Test-Path $path)) { throw "Backlog file not found: $path" }
    $content = Get-Content $path -Raw
    if (Get-Command ConvertFrom-Yaml -ErrorAction SilentlyContinue) {
        $yaml = $content | ConvertFrom-Yaml
        return $yaml.features
    }
    else {
        Require-Cmd "yq" "Install yq: winget install --id mikefarah.yq -e  (or: choco install yq -y)"
        $featuresJson = & yq eval -o=json '.features' $path
        return $featuresJson | ConvertFrom-Json
    }
}

function Ensure-Labels {
    param([string]$LabelsFile)

    if ($NoLabelSeeding) { return }

    $defaultMap = [ordered]@{
        "type:feature"       = "1d76db"
        "type:task"          = "6f42c1"
        "area:core"          = "0052cc"
        "area:systems"       = "0e8a16"
        "area:data"          = "5319e7"
        "area:ui"            = "c5def5"
        "area:services"      = "fbca04"
        "area:meta"          = "bfe5bf"
        "area:infra"         = "f9d0c4"
        "status:ready"       = "c2e0c6"
        "status:in-progress" = "fbca04"
        "status:review"      = "fef2c0"
        "status:blocked"     = "d93f0b"
    }

    $labels = @()
    if (Test-Path $LabelsFile) {
        try {
            $labels = Get-Content $LabelsFile -Raw | ConvertFrom-Json
            Write-Host "Seeding labels from $LabelsFile..."
            foreach ($label in $labels) {
                gh label create $label.name --color $label.color 2>$null | Out-Null
            }
            return
        }
        catch {
            Write-Warning "Could not seed labels from ${LabelsFile}: $_"
        }
    }

    Write-Host "Seeding default labels..."
    foreach ($kv in $defaultMap.GetEnumerator()) {
        gh label create $kv.Key --color $kv.Value 2>$null | Out-Null
    }
}

function Find-ExistingIssueByTitle([string]$title) {
    $json = gh issue list --state all --search "in:title `"$title`"" --limit 100 --json number, title 2>$null
    if (-not $json) { return $null }
    $arr = $json | ConvertFrom-Json
    foreach ($it in $arr) {
        if ($it.title -eq $title) { return $it.number }
    }
    return $null
}

function Ensure-Milestone([string]$title) {
    if ([string]::IsNullOrWhiteSpace($title)) { return $null }
    $repo = Get-RepoSlug
    $existing = gh api repos/$repo/milestones --method GET --paginate --silent 2>$null | ConvertFrom-Json
    $hit = $existing | Where-Object { $_.title -eq $title } | Select-Object -First 1
    if ($hit) { return $hit.number }
    # create
    $created = gh api repos/$repo/milestones -f title="$title" --method POST | ConvertFrom-Json
    return $created.number
}

# --- Main ---
Require-Cmd "gh" "Install GitHub CLI: winget install --id GitHub.cli -e"
Get-RepoSlug | Out-Null

$features = Read-BacklogFeatures -path $Backlog
if (-not $features) { throw "No features found in $Backlog (expected a top-level 'features:' array)." }

Ensure-Labels -LabelsFile ".github/labels.json"

$milestoneNumber = $null
if ($Milestone) {
    Write-Host "Ensuring milestone: $Milestone"
    $milestoneNumber = Ensure-Milestone -title $Milestone
}

Write-Host "Creating issues from $Backlog..."
foreach ($feature in $features) {
    $title = $feature.title
    if (-not $title) { continue }
    $labels = @()
    if ($feature.labels) { $labels = @($feature.labels) }
    $labelsCsv = ($labels -join ",")
    $spec = $feature.spec
    $tasks = if ($feature.tasks) { $feature.tasks } else { "- [ ] Data`n- [ ] Runtime`n- [ ] UI`n- [ ] Save/Load`n- [ ] Analytics`n- [ ] Tests" }
    $prompts = $feature.prompts
    $phase = $feature.phase

    $body = @"
# Phase
$phase

### Spec / Acceptance
$spec

### Implementation Tasks
$tasks

### Codex Prompt
$prompts
"@

    if ($DryRun) {
        Write-Host "[DryRun] Would create: $title"
        continue
    }

    $existing = Find-ExistingIssueByTitle -title $title
    if ($existing) {
        Write-Host "Exists (#$existing): $title"
        continue
    }

    $tmp = New-TemporaryFile
    # Force UTF8 (no BOM) to avoid weird quoting on Windows
    $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($tmp, $body, $utf8NoBom)

    $args = @("--title", $title, "--label", $labelsCsv, "--body-file", $tmp)
    if ($DefaultAssignee) { $args += @("--assignee", $DefaultAssignee) }
    if ($milestoneNumber) { $args += @("--milestone", $milestoneNumber) }

    gh issue create @args | Out-Null
    Remove-Item $tmp -Force

    Write-Host "Created: $title"
}

Write-Host "Done."
