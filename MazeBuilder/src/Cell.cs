using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class Cell
    {
        public int XCoOrd { get; private set; }
        public int YCoOrd { get; private set; }
        public bool Visited { get; set; }
        public bool IsPath { get; protected set; }
        public string StringRepresentation { get; private set; }

        public Cell(int xCoOrd, int yCoOrd, string representation)
        {
            XCoOrd = xCoOrd;
            YCoOrd = yCoOrd;
            StringRepresentation = representation;
        }
    }
}
