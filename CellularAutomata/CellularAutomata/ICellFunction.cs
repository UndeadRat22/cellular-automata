using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CellularAutomata.Extensions;

namespace CellularAutomata
{
    public interface ICellFunction
    {
        Cell Calculate(in PositionedCell previous, IEnumerable<PositionedCell> interactableCells);
    }

    public class ConstantCellFunction : ICellFunction
    {
        public Cell Calculate(in PositionedCell previous, IEnumerable<PositionedCell> interactableCells) => previous.Cell;
    }

    public class AveragingCellFunction : ICellFunction
    {
        public Cell Calculate(in PositionedCell previous, IEnumerable<PositionedCell> interactableCells)
        {
            var average = interactableCells.Average(n => n.Cell.Value).Floor();
            var cell = new Cell
            {
                Value = average
            };
            return cell;
        }
    }
}