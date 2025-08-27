using System.Linq;

namespace IdleSkiller.Systems
{
    public class InventoryService
    {
        private readonly SaveData _saveData;

        public InventoryService(SaveData saveData)
        {
            _saveData = saveData;
        }

        public int Capacity => _saveData.InventoryCapacity;

        public int UsedSpace => _saveData.Inventory.Values.Sum();

        public bool AddItem(string itemId, int quantity)
        {
            if (quantity <= 0)
                return false;

            if (UsedSpace + quantity > Capacity)
                return false;

            if (_saveData.Inventory.ContainsKey(itemId))
                _saveData.Inventory[itemId] += quantity;
            else
                _saveData.Inventory[itemId] = quantity;

            return true;
        }

        public bool RemoveItem(string itemId, int quantity)
        {
            if (!HasItems(itemId, quantity))
                return false;

            _saveData.Inventory[itemId] -= quantity;
            if (_saveData.Inventory[itemId] <= 0)
                _saveData.Inventory.Remove(itemId);

            return true;
        }

        public bool HasItems(string itemId, int quantity)
        {
            return _saveData.Inventory.TryGetValue(itemId, out var current) && current >= quantity;
        }
    }
}
