using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    interface IMapper
    {
        Path Map(Wall cell);
    }
}
