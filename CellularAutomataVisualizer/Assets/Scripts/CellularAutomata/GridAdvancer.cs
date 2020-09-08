using System;
using System.Collections.Generic;
using CellularAutomata.Grids;
using CellularAutomata.Grids.Cells;
using UnityEngine;

namespace CellularAutomata
{
    public class GridAdvancer
    {
        private AutomataGrid _previousAutomataGrid;
        public AutomataGrid AutomataGrid { get; set; }
        public List<ICellFunction> CellFunctions { get; } = new List<ICellFunction>
        {
            new AveragingCellFunction()
        };

        public GridAdvancer(AutomataGrid automataGrid)
        {
            AutomataGrid = automataGrid;
            _previousAutomataGrid = automataGrid.Copy();
        }

        /// <summary>
        /// Key point in this method is to reuse "previous" grid memory in order to speed up the process.
        /// </summary>
        public void Advance()
        {
            Debug.Log(2);
            var cells = AutomataGrid.GetAllCells();
            foreach (var cell in cells)
            {
                Debug.Log(1);
                var processedCell = cell;
                foreach (var cellFunction in CellFunctions)
                {
                    Debug.Log(3);
                    processedCell = cellFunction.Calculate(processedCell);
                    Debug.Log(4);
                    var cellToUpdate = _previousAutomataGrid.GetCellUnsafe(processedCell.Position.x, processedCell.Position.y, processedCell.Position.z);
                    cellToUpdate.Data = processedCell.Data;
                }
            }
            //swap _prev with current
            var temp = AutomataGrid;
            AutomataGrid = _previousAutomataGrid;
            _previousAutomataGrid = temp;
        }
    }
}