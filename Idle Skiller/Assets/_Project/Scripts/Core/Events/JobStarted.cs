namespace IdleSkiller.Core.Events
{
    public readonly struct JobStarted
    {
        public readonly string JobId;

        public JobStarted(string jobId)
        {
            JobId = jobId;
        }
    }
}
