using System;
using System.Collections.Generic;

namespace MazeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Maze Generator!");
            Console.WriteLine("");
            var maze = new Maze(50);

            Printer.PrintMultiDimensionalArray(maze._grid);

            Console.WriteLine("");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
