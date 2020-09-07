using System.Collections;
using System.Collections.Generic;

namespace CellularAutomata.Iterators
{
    public static class IterationProvider
    {
        public static IEnumerable<(int x, int y, int z)> Iterate(
            (int x, int y, int z) start,
            (int x, int y, int z) end)
        {
            for (var x = start.x; x < end.x; x++)
            {
                for (var y = start.y; y < end.y; y++)
                {
                    for (var z = start.z; z < end.z; z++)
                    {
                        yield return (x, y, z);
                    }
                }
            }
        }
    }
}