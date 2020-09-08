using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        public static (double min, double max) GetMinAndMaxValues<T>(this IEnumerable<T> enumerable, Func<T, double> elementSelector)
        {
            if (!enumerable.Any())
            {
                throw new Exception("Empty collection");
            }
            double min = double.MaxValue, max = double.MinValue;
            foreach (var element in enumerable)
            {
                var value = elementSelector(element);
                min = value < min ? value : min;
                max = value > max ? value : max;
            }

            return (min, max);
        }
    }
}