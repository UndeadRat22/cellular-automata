namespace CellularAutomata.Grids.Cells
{
    public class Cell
    {
        public CellData Data;
        public (int x, int y, int z) Position;
        
        public Cell[] Neighbors;

        public Cell Copy()
        {
            return new Cell
            {
                Data = Data,
                Position = Position,
                Neighbors = null,
            };
        }
    }
}