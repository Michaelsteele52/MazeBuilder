using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeBuilder
{
    public class Maze
    {
        private IMapper _mapper;
        public Cell[,] _grid { get; private set; }

        public List<Cell> _walls { get; private set; }

        public Maze(int size)
        {
            if (size < 4)
            {
                Console.WriteLine("Please request a bigger maze");
                return;
            }

            _mapper = new WallToPathMapper();   
            _grid = new Cell[size, size];
            PopulateStartupGrid();
            Generate();
            CreateEntrance(size, size);
        }

        private void PopulateStartupGrid()
        {
            for(var i = 0; i< _grid.GetLength(0); i++)
            {
                for(var j = 0; j< _grid.GetLength(1); j++)
                {
                    _grid[i,j] = new Wall(i, j);
                }
            }
        }

        private void Generate()
        {
            Random r = new Random();

            var width = _grid.GetLength(0)  - 1;
            var height = _grid.GetLength(1)  - 1;

            int startingHeight = r.Next(1, height);
            int startingWidth = r.Next(1, width);

            if (startingHeight == 0) startingHeight++;
            if (startingHeight == height) startingHeight--;
            if (startingWidth == 0) startingWidth++;
            if (startingWidth == width) startingWidth--;

            SetNewPath(_grid[startingWidth, startingHeight]);

            var walls = GenerateSurroundingWalls(startingWidth, startingHeight);

            while (walls.Any())
            {
                var index = r.Next(0, walls.GetLength());
                var randomWall = walls.GetByIndex(index);
                
                //Check if it is a left wall
                if (randomWall.XCoOrd  - 1 >= 0 ) {
                    if (!_grid[randomWall.XCoOrd - 1, randomWall.YCoOrd].Visited &&
                        _grid[randomWall.XCoOrd + 1, randomWall.YCoOrd].Visited)
                    {
                        if (SurroundingCells(randomWall) < 2)
                        {
                            SetNewPath(randomWall);

                            MarkUpperWall(randomWall, walls);
                            MarkBottomWall(randomWall, walls, height);
                            MarkLeftMostWall(randomWall, walls);
                        }

                        walls.DeleteExploredWalls(randomWall);
                    }
                }
                //Check if it is an upper wall
                if (randomWall.YCoOrd -1 >= 0) {
                    if (!_grid[randomWall.XCoOrd, randomWall.YCoOrd - 1].Visited &&
                        _grid[randomWall.XCoOrd, randomWall.YCoOrd + 1].Visited)
                    {
                        if (SurroundingCells(randomWall) < 2)
                        {
                            SetNewPath(randomWall);

                            MarkUpperWall(randomWall, walls);
                            MarkLeftMostWall(randomWall, walls);
                            MarkRightMostWall(randomWall, walls, width);
                        }
                        walls.DeleteExploredWalls(randomWall);
                    }
                }
                //Check if it is a bottom wall
                if (randomWall.YCoOrd < height) {
                    if (!_grid[randomWall.XCoOrd, randomWall.YCoOrd + 1].Visited &&
                        _grid[randomWall.XCoOrd, randomWall.YCoOrd - 1].Visited)
                    {
                        if (SurroundingCells(randomWall) < 2)
                        {
                            SetNewPath(randomWall);

                            MarkBottomWall(randomWall, walls, height);
                            MarkLeftMostWall(randomWall, walls);
                            MarkRightMostWall(randomWall, walls, width);
                        }
                        walls.DeleteExploredWalls(randomWall);
                    } 
                    
                }
                //Check if it is a right wall
                if (randomWall.XCoOrd +1 < width) {
                    if (!_grid[randomWall.XCoOrd + 1, randomWall.YCoOrd].Visited &&
                        _grid[randomWall.XCoOrd - 1, randomWall.YCoOrd].Visited)
                    {
                        if (SurroundingCells(randomWall) < 2)
                        {
                            SetNewPath(randomWall);

                            MarkUpperWall(randomWall, walls);
                            MarkRightMostWall(randomWall, walls, width);
                            MarkBottomWall(randomWall, walls, height);
                        }
                        walls.DeleteExploredWalls(randomWall);
                    }
                }

                walls.DeleteExploredWalls(randomWall);
            }
        }

        private void MarkUpperWall(Cell randomWall, Walls walls)
        {
            if (randomWall.YCoOrd != 0)
            {
                if (!_grid[randomWall.XCoOrd, randomWall.YCoOrd - 1].IsPath)
                {
                    _grid[randomWall.XCoOrd, randomWall.YCoOrd - 1].Visited = true;
                }
                if (!walls.Any() || !walls.Any(x => x?.XCoOrd == randomWall.XCoOrd && x?.YCoOrd == randomWall.YCoOrd - 1))
                {
                    if(randomWall.YCoOrd-1 > 0){
                        walls.AddToWalls(_grid[randomWall.XCoOrd, randomWall.YCoOrd - 1] as Wall);
                    }
                }
            }
        }

        private void MarkBottomWall(Cell randomWall, Walls walls, int height)
        {
            if (randomWall.YCoOrd != height - 1)
            {
                if (!_grid[randomWall.XCoOrd, randomWall.YCoOrd + 1].IsPath)
                {
                    _grid[randomWall.XCoOrd, randomWall.YCoOrd + 1].Visited = true;
                }
                if (!walls.Any() || !walls.Any(x => x?.XCoOrd == randomWall.XCoOrd && x?.YCoOrd == randomWall.YCoOrd + 1))
                {
                    if(randomWall.YCoOrd + 1 <= height)
                    {
                        walls.AddToWalls(_grid[randomWall.XCoOrd, randomWall.YCoOrd + 1] as Wall);
                    }
                }
            }
        }

        private void MarkLeftMostWall(Cell randomWall, Walls walls)
        {
            if(randomWall.XCoOrd != 0)
            {
                if(!_grid[randomWall.XCoOrd - 1, randomWall.YCoOrd].IsPath)
                {
                    _grid[randomWall.XCoOrd - 1, randomWall.YCoOrd].Visited = true;
                }
                if(!walls.Any() || !walls.Any(x => x?.XCoOrd - 1 == randomWall.XCoOrd && x?.YCoOrd == randomWall.YCoOrd))
                {
                    if(randomWall.XCoOrd - 1 > 0)
                    {
                        walls.AddToWalls(_grid[randomWall.XCoOrd - 1, randomWall.YCoOrd] as Wall);
                    }
                }
            }
        }

        private void MarkRightMostWall(Cell randomWall, Walls walls, int width)
        {
            if (randomWall.XCoOrd != width - 1)
            {
                if (!_grid[randomWall.XCoOrd + 1, randomWall.YCoOrd].IsPath)
                {
                    _grid[randomWall.XCoOrd + 1, randomWall.YCoOrd].Visited = true;
                }
                if (!walls.Any() || !walls.Any(x => x?.XCoOrd == randomWall.XCoOrd + 1 && x?.YCoOrd == randomWall.YCoOrd))
                {
                    if(randomWall.XCoOrd +1 < width)
                    {
                        walls.AddToWalls(_grid[randomWall.XCoOrd + 1, randomWall.YCoOrd] as Wall);
                    }
                }
            }
        }

        private void SetNewPath(Cell cell)
        {
            _grid[cell.XCoOrd, cell.YCoOrd] = _mapper.Map(cell as Wall);
        }

        private int SurroundingCells(Wall wall)
        {
            var surroundingCells = 0;
            if (wall.XCoOrd - 1 > 0)
            {
                if (_grid[wall.XCoOrd - 1, wall.YCoOrd].IsPath) surroundingCells++;
            }
            if (wall.XCoOrd + 1 < _grid.GetLength(0) - 1)
            {
                if (_grid[wall.XCoOrd + 1, wall.YCoOrd].IsPath) surroundingCells++;
            }
            if (wall.YCoOrd - 1 > 0)
            {
                if (_grid[wall.XCoOrd, wall.YCoOrd - 1].IsPath) surroundingCells++;
            }
            if (wall.YCoOrd + 1 < _grid.GetLength(1))
            {
                if (_grid[wall.XCoOrd, wall.YCoOrd + 1].IsPath) surroundingCells++;
            }

            return surroundingCells;
        }

        private Walls GenerateSurroundingWalls(int width, int height)
        {
            _grid[height - 1, width] = new Wall(width, height - 1, true);
            _grid[height, width - 1] = new Wall(width - 1, height, true);
            _grid[height + 1, width] = new Wall(width, height + 1, true);
            _grid[height, width + 1] = new Wall(width + 1, height, true);

            var walls = new Walls();
            walls.AddToWalls(new Wall(width, height - 1));
            walls.AddToWalls(new Wall(width - 1, height));
            walls.AddToWalls(new Wall(width, height + 1));
            walls.AddToWalls(new Wall(width + 1,height));

            return walls;
        }

        private void CreateEntrance(int width, int height)
        {
            for(var i = 0; i < width; i++)
            {
                if(_grid[1, i].IsPath)
                {
                    _grid[0, i] = new Path(0, i, true);
                    break;
                }
            }
            for (var i = width - 1; i > 0; i--)
            {
                if(_grid[height - 2, i].IsPath)
                {
                    _grid[height - 1, i] = new Path(height - 1, i, true);
                    break;
                }
            }
        }
    }
}
