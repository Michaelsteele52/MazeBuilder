using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class Cell
    {
        public int XCoOrd { get; set; }
        public int YCoOrd { get; set; }
        public bool Visited { get; set; }
        public bool IsPath = false;
        public string StringRepresentation { get; set; }

        public Cell(int xCoOrd, int yCoOrd, string representation)
        {
            XCoOrd = xCoOrd;
            YCoOrd = yCoOrd;
            StringRepresentation = representation;
        }
    }
}
