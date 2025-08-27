namespace IdleSkiller.Systems.Gathering
{
    public interface IGatheringNode
    {
        string OutputItemId { get; }
        float GetDuration(int tier);
        int GetYield(int tier);
    }
}
