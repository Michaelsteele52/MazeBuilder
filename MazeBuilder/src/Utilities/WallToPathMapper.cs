using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class WallToPathMapper : IMapper
    {
        public Path Map(Wall wall)
        {
            return new Path(
                wall.XCoOrd,
                wall.YCoOrd,
                true);
        }
    }
}
