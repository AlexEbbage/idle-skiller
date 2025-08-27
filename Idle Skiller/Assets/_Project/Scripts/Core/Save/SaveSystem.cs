using UnityEngine;

namespace IdleSkiller.Core.Save
{
    public class SaveSystem : ISaveSystem
    {
        private const string PlayerPrefsKey = "save";

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKey))
            {
                return new SaveData();
            }

            var json = PlayerPrefs.GetString(PlayerPrefsKey);
            var data = new SaveData();
            JsonUtility.FromJsonOverwrite(json, data);

            if (data.Version != SaveData.CURRENT_VERSION)
            {
                data = Migrate(data);
            }

            return data;
        }

        public void Save(SaveData data)
        {
            data.Version = SaveData.CURRENT_VERSION;
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(PlayerPrefsKey, json);
            PlayerPrefs.Save();
        }

        public bool HasSave()
        {
            return PlayerPrefs.HasKey(PlayerPrefsKey);
        }

        private SaveData Migrate(SaveData data)
        {
            // Placeholder for future data migrations
            data.Version = SaveData.CURRENT_VERSION;
            return data;
        }
    }
}
