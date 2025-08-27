namespace IdleSkiller.Systems
{
    public class CurrencyService
    {
        private readonly SaveData _saveData;

        public CurrencyService(SaveData saveData)
        {
            _saveData = saveData;
        }

        public int Gold => _saveData.Gold;
        public int Gems => _saveData.Gems;
        public int Fame => _saveData.Fame;

        public void AddGold(int amount) => _saveData.Gold += amount;
        public bool SpendGold(int amount)
        {
            if (_saveData.Gold < amount) return false;
            _saveData.Gold -= amount;
            return true;
        }

        public void AddGems(int amount) => _saveData.Gems += amount;
        public bool SpendGems(int amount)
        {
            if (_saveData.Gems < amount) return false;
            _saveData.Gems -= amount;
            return true;
        }

        public void AddFame(int amount) => _saveData.Fame += amount;
        public bool SpendFame(int amount)
        {
            if (_saveData.Fame < amount) return false;
            _saveData.Fame -= amount;
            return true;
        }
    }
}
