using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Extensions;
using CellularAutomata.Grids.Cells;
using CellularAutomata.Iterators;

namespace CellularAutomata.Grids
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



        public IEnumerable<Cell> GetCellNeighbors((int x, int y, int z) cell)
        {
            var (x, y, z) = cell;
            foreach (var xOffset in Enumerable.Range(-1, 3))
            {
                var nX = x + xOffset;
                foreach (var yOffset in Enumerable.Range(-1, 3))
                {
                    var nY = y + yOffset;
                    foreach (var zOffset in Enumerable.Range(-1, 3))
                    {
                        //maybe self should be averaged too idk;
                        if (xOffset == 0 && yOffset == 0 && zOffset == 0)
                            continue;

                        var nZ = z + zOffset;
                        yield return GetCellUnsafe(nX, nY, nZ);
                    }
                }
            }
        }

        public IEnumerable<Cell> GetAllCells()
        {
            foreach (var (x, y, z) in IterationProvider.Iterate((0, 0, 0), (Size.x, Size.y, Size.z)))
            {
                yield return GetCellUnsafe(x, y, z);
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
            foreach (Cell cell in Cells)
            {
                var cellCopy = cell.Copy();
                copy.SetCellUnsafe(cellCopy, cellCopy.Position);
            }
            copy.AssignNeighbors();
            return copy;
        }

        private void AssignNeighbors()
        {
            foreach (var(x, y, z) in IterationProvider.Iterate((0, 0, 0), (Size.x, Size.y, Size.z)))
            {
                var cell = GetCellUnsafe(x, y, z);
                var interactableCells = GetCellNeighbors(cell.Position);
                cell.Neighbors = interactableCells.ToArray();
            }
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