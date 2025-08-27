using System.Collections.Generic;
using IdleSkiller.Systems.Workers;

namespace IdleSkiller.Systems
{
    public class SaveData
    {
        public int Gold { get; set; }
        public int Gems { get; set; }
        public int Fame { get; set; }

        public int InventoryCapacity { get; set; } = 0;

        public Dictionary<string, int> Inventory { get; set; } = new Dictionary<string, int>();

        public List<Worker> Workers { get; set; } = new List<Worker>();
    }
}
