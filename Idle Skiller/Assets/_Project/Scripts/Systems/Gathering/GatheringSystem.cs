using System;

namespace IdleSkiller.Systems.Gathering
{
    public class GatheringSystem
    {
        private readonly InventoryService _inventory;
        private readonly float _durationMultiplier;
        private readonly float _yieldMultiplier;

        public event Action<string>? Telemetry;

        public GatheringSystem(InventoryService inventory, float durationMultiplier = 1f, float yieldMultiplier = 1f)
        {
            _inventory = inventory;
            _durationMultiplier = durationMultiplier;
            _yieldMultiplier = yieldMultiplier;
        }

        public float GetDuration(IGatheringNode node, int tier)
        {
            return node.GetDuration(tier) * _durationMultiplier;
        }

        public int Gather(IGatheringNode node, int tier)
        {
            Telemetry?.Invoke("run");
            var amount = (int)MathF.Round(node.GetYield(tier) * _yieldMultiplier);
            _inventory.AddItem(node.OutputItemId, amount);
            Telemetry?.Invoke("complete");
            return amount;
        }
    }
}
