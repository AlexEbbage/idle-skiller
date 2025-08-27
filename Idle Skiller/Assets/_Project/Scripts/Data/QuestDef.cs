using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/Quest")]
    public class QuestDef : ScriptableObject
    {
        [System.Serializable]
        public struct ItemStack
        {
            public ItemDef Item;
            public int Amount;
        }

        public string Id;
        public string DisplayName;
        [TextArea] public string Description;
        public ItemStack[] Rewards;
    }
}
