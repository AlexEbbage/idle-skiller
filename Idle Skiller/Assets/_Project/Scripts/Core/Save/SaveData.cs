using System;
using System.Collections.Generic;

namespace IdleSkiller.Core.Save
{
    [Serializable]
    public class SaveData
    {
        public const int CURRENT_VERSION = 1;

        public int Version = CURRENT_VERSION;
        public int Gold;
        public int Gems;
        public int Fame;
        public List<string> Inventory = new List<string>();
        public int Workers;
        public string Guild;
    }
}
