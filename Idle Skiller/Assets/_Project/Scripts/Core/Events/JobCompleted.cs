namespace IdleSkiller.Core.Events
{
    public readonly struct JobCompleted
    {
        public readonly string JobId;

        public JobCompleted(string jobId)
        {
            JobId = jobId;
        }
    }
}
