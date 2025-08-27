using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/LootTable")]
    public class LootTable : ScriptableObject
    {
        [System.Serializable]
        public struct Entry
        {
            public ItemDef Item;
            public float Weight;
        }

        public Entry[] Entries;
    }
}
