using System.Collections.Generic;
using IdleSkiller.Systems;
using IdleSkiller.Systems.Gathering;
using NUnit.Framework;

namespace Tests
{
    public class GatheringSystemTests
    {
        private SaveData _saveData = null!;
        private InventoryService _inventory = null!;
        private GatheringSystem _system = null!;

        private class TestNode : IGatheringNode
        {
            private readonly float[] _durations;
            private readonly int[] _yields;
            public string OutputItemId { get; }

            public TestNode(string itemId, float[] durations, int[] yields)
            {
                OutputItemId = itemId;
                _durations = durations;
                _yields = yields;
            }

            public float GetDuration(int tier) => _durations[tier - 1];
            public int GetYield(int tier) => _yields[tier - 1];
        }

        [SetUp]
        public void Setup()
        {
            _saveData = new SaveData { InventoryCapacity = 100 };
            _inventory = new InventoryService(_saveData);
            _system = new GatheringSystem(_inventory, durationMultiplier: 2f, yieldMultiplier: 1.5f);
        }

        [Test]
        public void GetDuration_UsesMultiplier()
        {
            var node = new TestNode("wood", new[] { 5f, 10f, 15f }, new[] { 1, 2, 3 });
            var duration = _system.GetDuration(node, 2);
            Assert.AreEqual(20f, duration);
        }

        [Test]
        public void Gather_AddsItemsWithYieldMultiplier()
        {
            var node = new TestNode("stone", new[] { 6f, 12f, 18f }, new[] { 2, 4, 6 });
            var added = _system.Gather(node, 1);
            Assert.AreEqual(3, added);
            Assert.True(_inventory.HasItems("stone", 3));
        }

        [Test]
        public void Gather_EmitsTelemetry()
        {
            var node = new TestNode("ore", new[] { 10f, 20f, 30f }, new[] { 1, 2, 3 });
            var events = new List<string>();
            _system.Telemetry += events.Add;
            _system.Gather(node, 1);
            CollectionAssert.AreEqual(new[] { "run", "complete" }, events);
        }
    }
}
