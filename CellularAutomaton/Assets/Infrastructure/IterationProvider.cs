using System.Linq;
using Assets.Infrastructure.Extensions;
using UnityEngine;

namespace Assets.Infrastructure
{
    public static class IterationProvider
    {
        static IterationProvider()
        {
            MooreVectors = (new Vector3Int(-1, -1, -1))
                .IterateToInclusive(new Vector3Int(1, 1, 1))
                .Distinct()
                .ToArray();

            VonNeumannVectors =
                (new Vector3Int(-1, 0, 0))
                .IterateToInclusive(new Vector3Int(1, 0, 0))
                .Concat(
                    (new Vector3Int(0, -1, 0))
                    .IterateToInclusive(new Vector3Int(0, 1, 0)))
                .Concat(
                    (new Vector3Int(0, 0, -1))
                    .IterateToInclusive(new Vector3Int(0, 0, 1)))
                .Distinct()
                .ToArray();
        }
        public static Vector3Int[] MooreVectors { get; }
        public static Vector3Int[] VonNeumannVectors { get; }
    }
}