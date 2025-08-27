namespace IdleSkiller.Systems.Workers
{
    public static class WorkerFactory
    {
        public static Worker CreateInitial()
        {
            return new Worker
            {
                Name = "Worker",
                State = WorkerState.Idle
            };
        }
    }
}
