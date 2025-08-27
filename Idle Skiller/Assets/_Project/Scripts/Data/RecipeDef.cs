using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/Recipe")]
    public class RecipeDef : ScriptableObject
    {
        [System.Serializable]
        public struct ItemStack
        {
            public ItemDef Item;
            public int Amount;
        }

        public string Id;
        public ItemStack[] Ingredients;
        public ItemStack Result;
    }
}
