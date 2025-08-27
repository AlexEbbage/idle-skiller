using IdleSkiller.Systems;
using NUnit.Framework;

namespace Tests
{
    public class InventoryServiceTests
    {
        private SaveData _saveData;
        private InventoryService _inventory;

        [SetUp]
        public void Setup()
        {
            _saveData = new SaveData { InventoryCapacity = 10 };
            _inventory = new InventoryService(_saveData);
        }

        [Test]
        public void AddItem_Fails_When_OverCapacity()
        {
            var result = _inventory.AddItem("wood", 11);
            Assert.False(result);
            Assert.False(_inventory.HasItems("wood", 1));
        }

        [Test]
        public void AddItem_Succeeds_Within_Capacity()
        {
            var result = _inventory.AddItem("wood", 5);
            Assert.True(result);
            Assert.True(_inventory.HasItems("wood", 5));
        }

        [Test]
        public void RemoveItem_Fails_When_Insufficient()
        {
            _inventory.AddItem("stone", 3);
            var result = _inventory.RemoveItem("stone", 5);
            Assert.False(result);
            Assert.True(_inventory.HasItems("stone", 3));
        }

        [Test]
        public void RemoveItem_Succeeds_When_Available()
        {
            _inventory.AddItem("stone", 5);
            var result = _inventory.RemoveItem("stone", 3);
            Assert.True(result);
            Assert.True(_inventory.HasItems("stone", 2));
        }
    }
}
