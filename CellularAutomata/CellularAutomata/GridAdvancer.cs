using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Grids;

namespace CellularAutomata
{
    public class GridAdvancer
    {
        public Grid Grid { get; set; }
        public List<ICellFunction> CellFunctions { get; } = new List<ICellFunction>
        {
            new AveragingCellFunction()
        };

        public GridAdvancer(Grid grid)
        {
            Grid = grid;
        }

        public void Advance()
        {
            var grid = Grid.Copy();
            foreach (var positionedCell in grid.GetAllCells())
            {
                var interactableCells = grid.GetCellNeighbors(positionedCell).ToList();
                foreach (var cellFunction in CellFunctions)
                {
                    var nextCell = cellFunction.Calculate(positionedCell, interactableCells);
                    grid.SetCellUnsafe(nextCell, positionedCell.Position);
                }
            }
            Grid = grid;
        }
    }
}