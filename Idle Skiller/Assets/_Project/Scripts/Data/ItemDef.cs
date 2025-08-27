using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/Item")]
    public class ItemDef : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        [TextArea] public string Description;
        public Sprite Icon;
        public int MaxStack = 1;
    }
}
