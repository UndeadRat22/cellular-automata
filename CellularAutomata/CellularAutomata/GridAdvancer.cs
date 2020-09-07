using System.Collections.Generic;
using CellularAutomata.Grids;
using CellularAutomata.Grids.Cells;

namespace CellularAutomata
{
    public class GridAdvancer
    {
        private Grid _previousGrid;
        public Grid Grid { get; set; }
        public List<ICellFunction> CellFunctions { get; } = new List<ICellFunction>
        {
            new AveragingCellFunction()
        };

        public GridAdvancer(Grid grid)
        {
            Grid = grid;
            _previousGrid = grid.Copy();
        }

        /// <summary>
        /// Key point in this method is to reuse "previous" grid memory in order to speed up the process.
        /// </summary>
        public void Advance()
        {
            var cells = Grid.GetAllCells();
            foreach (var cell in cells)
            {
                var processedCell = cell;
                foreach (var cellFunction in CellFunctions)
                {
                    processedCell = cellFunction.Calculate(processedCell);
                    var cellToUpdate = _previousGrid.GetCellUnsafe(processedCell.Position.x, processedCell.Position.y, processedCell.Position.z);
                    cellToUpdate.Data = processedCell.Data;
                }
            }
            //swap _prev with current
            var temp = Grid;
            Grid = _previousGrid;
            _previousGrid = temp;
        }
    }
}