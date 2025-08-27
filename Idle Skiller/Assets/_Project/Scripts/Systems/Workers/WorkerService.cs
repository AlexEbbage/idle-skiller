using System.Collections.Generic;
using IdleSkiller.Systems;

namespace IdleSkiller.Systems.Workers
{
    public class WorkerService
    {
        private readonly SaveData _saveData;

        public WorkerService(SaveData saveData)
        {
            _saveData = saveData;
            if (_saveData.Workers.Count == 0)
            {
                _saveData.Workers.Add(WorkerFactory.CreateInitial());
            }
        }

        public IReadOnlyList<Worker> Workers => _saveData.Workers;

        public Worker AddWorker(Worker worker)
        {
            _saveData.Workers.Add(worker);
            return worker;
        }
    }
}
