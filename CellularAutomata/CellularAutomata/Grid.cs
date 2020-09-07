using System.Collections.Generic;
using System.Linq;

namespace CellularAutomata
{
    public class Grid
    {
        public (int x, int y, int z) Size { get; }
        public Grid(in (int x, int y, int z) size)
        {
            Size = size;
            Cells = new Cell[size.x, size.y, size.z];
        }
        public Cell[,,] Cells { get; set; }

        public Cell this[in int x, in int y, in int z] => Cells
            [y.Wrap(Size.y), x.Wrap(Size.x), z.Wrap(Size.z)];

        public IEnumerable<Cell> GetCellNeighbors(PositionedCell cell)
        {
            var (x, y, z) = cell.Position;
            foreach (var xOffset in Enumerable.Range(-1, 3))
            {
                foreach (var yOffset in Enumerable.Range(-1, 3))
                {
                    foreach (var zOffset in Enumerable.Range(-1, 3))
                    {
                        //maybe self should be averaged too idk;
                        if (xOffset == 0 && yOffset == 0 && zOffset == 0)
                            continue;

                        yield return this[x + xOffset, y + yOffset, z + zOffset];
                    }
                }
            }
        }

        public IEnumerable<PositionedCell> GetAllCells()
        {
            foreach (var x in Enumerable.Range(0, Size.x))
            {
                foreach (var y in Enumerable.Range(0, Size.y))
                {
                    foreach (var z in Enumerable.Range(0, Size.z))
                    {
                        yield return new PositionedCell
                        {
                            Cell = GetCellUnsafe(x, y, z),
                            Position = (x, y, z)
                        };
                    }
                }
            }
        }

        public Cell GetCellUnsafe(in int x, in int y, in int z)
        {
            return Cells[x, y, z];
        }

        public void SetCellUnsafe(in Cell cell, in (int x, int y, int z) position)
        {
            Cells[position.x, position.y, position.z] = cell;
        }

        public Grid Copy()
        {
            var copy = new Grid(Size);

            return copy;
        }
    }

    public class HorizontalLayer
    {
        public HorizontalLayer(int x, int z)
        {
            Cells = new Cell[x, z];
        }
        public Cell[,] Cells { get; set; }

        public Cell this[int x, int z]
        {
            get => Cells[x, z];
            set => Cells[x, z] = value;
        }
    }
}