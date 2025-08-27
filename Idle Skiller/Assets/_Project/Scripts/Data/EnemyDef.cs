using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/Enemy")]
    public class EnemyDef : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        public int MaxHealth;
        public LootTable Loot;
    }
}
