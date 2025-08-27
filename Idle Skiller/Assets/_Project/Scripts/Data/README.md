# Data Definitions

This folder contains ScriptableObject types used to configure game content. Designers can create new assets in `/Assets/_Project/Data` without code changes.

## SkillDef
* `Id` - unique identifier
* `DisplayName` - human readable name
* `Description` - text description
* `Icon` - sprite shown in UI

## ItemDef
* `Id` - unique identifier
* `DisplayName` - human readable name
* `Description` - text description
* `Icon` - sprite shown in UI
* `MaxStack` - maximum stack size

## RecipeDef
* `Id` - unique identifier
* `Ingredients` - list of input item stacks
* `Result` - output item stack

## QuestDef
* `Id` - unique identifier
* `DisplayName` - human readable name
* `Description` - text description
* `Rewards` - item stacks granted on completion

## EnemyDef
* `Id` - unique identifier
* `DisplayName` - human readable name
* `MaxHealth` - hit points
* `Loot` - loot table used on defeat

## LootTable
* `Entries` - weighted items that can drop
