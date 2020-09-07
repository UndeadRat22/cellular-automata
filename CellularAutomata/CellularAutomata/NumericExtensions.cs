using System;

namespace CellularAutomata
{
    public static class NumericExtensions
    {
        public static bool IsNegative(this in int n) => n < 0;
        public static bool IsPositive(this in int n) => n > 0;

        public static int Wrap(this in int value, in int max)
        {
            if (value.IsPositive())
            {
                return value % max;
            }
            int multiplier = Math.Abs(value / max) + 1;
            return (value + (max * multiplier) % max);
        }

        public static int Floor(this double n) => (int) n;
    }
}