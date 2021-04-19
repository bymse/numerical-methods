namespace Lab2
{
    public static class Extensions
    {
        public static decimal DivideSafely(this decimal first, decimal second)
        {
            if (second == 0)
                return 0;

            return first / second;
        }
    }
}