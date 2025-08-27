using System;

namespace IdleSkiller.Core.Save
{
    public interface ISaveSystem
    {
        SaveData Load();
        void Save(SaveData data);
        bool HasSave();
    }
}
