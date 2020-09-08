using UnityEngine;

namespace Assets.Grids.Cells
{
    public class Cell
    {
        public Vector3Int Position;

        public double Magnitude;
        public Vector3 Direction;

        public Cell[] Neighbors;

        public static Cell Default(Vector3Int position) => new Cell
        {
            Direction = Vector3.zero,
            Magnitude = GridSettings.MinMagnitude,
            Neighbors = null,
            Position = position
        };
    }
}