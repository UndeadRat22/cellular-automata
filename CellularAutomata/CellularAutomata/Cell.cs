namespace CellularAutomata
{
    public struct Cell
    {
        public int Value { get; set; }
        public (int, int, int) Direction { get; set; }
    }
}