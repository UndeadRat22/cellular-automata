using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Extensions;
using CellularAutomata.Grids.Cells;
using CellularAutomata.Iterators;
using UnityEngine;

namespace CellularAutomata.Grids
{
    public class AutomataGrid
    {
        public (int x, int y, int z) Size { get; }
        public AutomataGrid(in (int x, int y, int z) size)
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
                        yield return this[nX, nY, nZ];
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

        public void SetCellUnsafe(Cell cell)
        {
            Cells[cell.Position.x, cell.Position.y, cell.Position.z] = cell;
        }

        public AutomataGrid Copy()
        {
            var copy = new AutomataGrid(Size);
            foreach (Cell cell in Cells)
            {
                var cellCopy = cell.Copy();
                copy.SetCellUnsafe(cellCopy);
            }
            Debug.Log("This fails");
            copy.AssignNeighbors();
            Debug.Log("Nope");
            return copy;
        }

        public void AssignNeighbors()
        {
            foreach (var(x, y, z) in IterationProvider.Iterate((0, 0, 0), (Size.x, Size.y, Size.z)))
            {
                Debug.Log("hello ");
                var cell = GetCellUnsafe(x, y, z);
                Debug.Log(cell.Position);
                var interactableCells = GetCellNeighbors(cell.Position);
                cell.Neighbors = interactableCells.ToArray();
            }
        }
    }


}