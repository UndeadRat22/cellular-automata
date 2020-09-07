using System.Linq;

namespace CellularAutomata
{
    public interface ICellFunction
    {
        Cell Execute(in PositionedCell previous);
    }

    public class ConstantCellFunction : ICellFunction
    {
        public Cell Execute(in PositionedCell previous) => previous.Cell;
    }

    public class AveragingCellFunction : ICellFunction
    {
        private readonly Grid _grid;

        public AveragingCellFunction(Grid grid)
        {
            _grid = grid;
        }

        public Cell Execute(in PositionedCell previous)
        {
            var neighbors = _grid.GetCellNeighbors(previous);
            var average = neighbors.Average(n => n.Value).Floor();
            var cell = new Cell
            {
                Value = average
            };
            return cell;
        }
    }
}