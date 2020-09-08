using UnityEngine;

namespace Assets.Grids.Cells
{
    public class Cell
    {
        public Vector3Int Position;

        public double Magnitude;
        public Vector3 Direction;

        public Cell[] Region;

        public static Cell Default(Vector3Int position) => new Cell
        {
            Direction = Vector3.zero,
            Magnitude = GridSettings.MinMagnitude,
            Region = null,
            Position = position
        };
    }
}