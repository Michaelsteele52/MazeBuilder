using System;
using System.Collections.Generic;

namespace MazeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Maze Generator!");

            var maze = new Maze(100);

            var printer = new Printer();
            printer.PrintMultiDimensionalArray(maze._grid);
        }
    }
}
