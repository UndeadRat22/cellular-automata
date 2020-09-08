namespace Assets.Infrastructure.Extensions
{
    public static class NumericExtensions
    {
        public static int Wrap(this int value, int max) => (value % max + max) % max;
    }
}