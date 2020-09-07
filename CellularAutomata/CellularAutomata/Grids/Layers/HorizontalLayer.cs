using CellularAutomata.Grids.Cells;

namespace CellularAutomata.Grids.Layers
{
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