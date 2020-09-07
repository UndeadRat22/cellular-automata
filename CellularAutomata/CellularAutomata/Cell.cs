namespace CellularAutomata
{
    public struct Cell
    {
        public int Value { get; set; }
        public (int, int, int) Direction { get; set; }
    }

    public struct PositionedCell
    {
        public Cell Cell { get; set; }
        public (int x, int y, int z) Position { get; set; }
    }
}