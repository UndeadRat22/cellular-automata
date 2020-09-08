using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Grids;
using UnityEngine;

namespace Assets.Infrastructure.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3Int Wrap(this Vector3Int vector, int max) 
            => new Vector3Int(vector.x.Wrap(max), vector.y.Wrap(max), vector.z.Wrap(max));

        public static Vector3Int Wrap(this Vector3Int vector, Vector3Int max)
            => new Vector3Int(vector.x.Wrap(max.x), vector.y.Wrap(max.y), vector.z.Wrap(max.z));

        public static IEnumerable<Vector3Int> GetSurroundingVectors(
            this Vector3Int vector,
            GridSettings.SurroundSelectionType type)
        {
            IEnumerable<Vector3Int> result;
            switch (type)
            {
                case GridSettings.SurroundSelectionType.Moore:
                    result = IterationProvider.MooreVectors.Select(v => vector + v);
                    break;
                case GridSettings.SurroundSelectionType.VonNeumann:
                    result = IterationProvider.VonNeumannVectors.Select(v => vector + v);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            return result;
        }

        public static IEnumerable<Vector3Int> IterateTo(this Vector3Int from, Vector3Int to)
        {
            for (int x = from.x; x < to.x; x++)
            {
                for (int y = from.y; y < to.y; y++)
                {
                    for (int z = from.z; z < to.z; z++)
                    {
                        yield return new Vector3Int(x, y, z);
                    }
                }
            }
        }

        public static IEnumerable<Vector3Int> IterateToInclusive(this Vector3Int from, Vector3Int to)
        {
            for (int x = from.x; x <= to.x; x++)
            {
                for (int y = from.y; y <= to.y; y++)
                {
                    for (int z = from.z; z <= to.z; z++)
                    {
                        yield return new Vector3Int(x, y, z);
                    }
                }
            }
        }
    }
}