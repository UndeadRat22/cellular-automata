using System.Linq;
using CellularAutomata.Extensions;
using CellularAutomata.Grids.Cells;

namespace CellularAutomata
{
    public interface ICellFunction
    {
        Cell Calculate(in Cell previous);
    }

    public class ConstantCellFunction : ICellFunction
    {
        public Cell Calculate(in Cell previous) => previous;
    }

    public class AveragingCellFunction : ICellFunction
    {
        public Cell Calculate(in Cell previous)
        {
            var average = previous.Neighbors.Average(n => n.Data.Magnitude).Floor();
            var cell = new Cell
            {
                Data = new CellData
                {
                    Direction = previous.Data.Direction,//TODO
                    Magnitude = average
                }
            };
            return cell;
        }
    }
}