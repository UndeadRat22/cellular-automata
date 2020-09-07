using System;
using System.Collections.Generic;

namespace CellularAutomata
{
    public class GridAdvancer
    {
        public Grid PreviousGrid { get; set; }
        public Grid Grid { get; set; }
        public List<ICellFunction> CellFunctions { get; } = new List<ICellFunction>();

    }
}