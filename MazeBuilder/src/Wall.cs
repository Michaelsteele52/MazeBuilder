using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class Wall : Cell
    {
        public Wall(int xCoOrd, int yCoOrd, bool visited = false) : base(xCoOrd, yCoOrd, "#") {
            Visited = visited;
        }
    }
}
