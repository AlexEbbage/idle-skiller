using UnityEngine;

namespace IdleSkiller.Systems.Gathering
{
    [CreateAssetMenu(menuName = "IdleSkiller/Gathering/Node")]
    public class GatheringNodeDef : ScriptableObject, IGatheringNode
    {
        public string OutputItemId;
        public float[] Durations = new float[3];
        public int[] Yields = new int[3];

        public float GetDuration(int tier) => tier > 0 && tier <= Durations.Length ? Durations[tier - 1] : 0f;
        public int GetYield(int tier) => tier > 0 && tier <= Yields.Length ? Yields[tier - 1] : 0;
    }
}
