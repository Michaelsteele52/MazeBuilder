using System;
using System.Collections.Generic;
using System.Text;

namespace MazeBuilder
{
    public class Printer
    {
        public void PrintMultiDimensionalArray(Cell[,] input)
        {
            for(var i = 0; i < input.GetLength(0); i++)
            {
                for(var j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i,j].StringRepresentation);
                }
                Console.WriteLine();
            }
        }
    }
}
