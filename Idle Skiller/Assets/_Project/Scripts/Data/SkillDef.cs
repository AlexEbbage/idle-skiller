using UnityEngine;

namespace IdleSkiller.Data
{
    [CreateAssetMenu(menuName = "IdleSkiller/Skill")]
    public class SkillDef : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        [TextArea] public string Description;
        public Sprite Icon;
    }
}
