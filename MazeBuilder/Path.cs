using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class Path : Cell
    {
        public Path(int xCoOrd, int yCoOrd, bool visited = false) : base(xCoOrd, yCoOrd, " ")
        {
            Visited = visited;
            IsPath = true;
        }
    }
}
