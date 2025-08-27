namespace IdleSkiller.Core.Events
{
    public readonly struct CurrencyChanged
    {
        public readonly int Gold;
        public readonly int Gems;
        public readonly int Fame;

        public CurrencyChanged(int gold, int gems, int fame)
        {
            Gold = gold;
            Gems = gems;
            Fame = fame;
        }
    }
}
