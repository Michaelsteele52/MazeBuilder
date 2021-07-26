using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeBuilder
{
    public class Walls
    {
        private List<Wall> walls;

        public Walls()
        {
            walls = new List<Wall>();
        }

        public void AddToWalls(Wall wall)
        {
            if(wall == null)
            {
                return;
            }
            walls.Add(wall);
        }

        public List<Wall> GetWalls()
        {
            return walls;
        }

        public bool Any()
        {
            return walls.Any();
        }

        public bool Any(Func<Wall, bool> query)
        {
            return walls.Any(query);
        }

        public Wall GetByIndex(int index)
        {
            return walls[index];
        }

        public int GetLength()
        {
            return walls.Count;
        }

        public void DeleteExploredWalls(Wall randomWall)
        {
            foreach (var wall in walls.ToList())
            {
                if (wall == null)
                {
                    walls.Remove(wall);
                    continue;
                }
            }
            walls.Remove(randomWall);
        }
    }
}
