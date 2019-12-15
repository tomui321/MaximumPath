namespace MaximumPath
{
    public static class Extensions
    {
        public static bool IsEven(this long value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this long value)
        {
            return !value.IsEven();
        }
    }
}
