using System;
using System.Collections.Generic;

namespace IdleSkiller.Systems.Workers
{
    public enum WorkerState
    {
        Idle,
        Working,
        Cooldown
    }

    [Serializable]
    public class Worker
    {
        public string Name = string.Empty;
        public Dictionary<string, int> SkillLevels = new Dictionary<string, int>();
        public Dictionary<string, string> EquipmentSlots = new Dictionary<string, string>();
        public WorkerState State = WorkerState.Idle;

        private const float LevelPercent = 0.01f;

        private int GetLevel(string skill) => SkillLevels.TryGetValue(skill, out var lvl) ? lvl : 0;

        public float GetSpeedPercent(string skill) => 1f + GetLevel(skill) * LevelPercent;
        public float GetYieldPercent(string skill) => 1f + GetLevel(skill) * LevelPercent;
        public float GetCritPercent(string skill) => GetLevel(skill) * LevelPercent;
        public float GetXpPercent(string skill) => 1f + GetLevel(skill) * LevelPercent;
    }
}
