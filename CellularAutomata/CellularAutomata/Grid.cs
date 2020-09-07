namespace CellularAutomata
{
    public class Grid
    {
        public Grid(int x, int y, int z)
        {
            Layers = new Layer[y];
        }
        public Layer[] Layers { get; set; }
    }

    public struct Layer
    {
        public Layer(int x, int z)
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