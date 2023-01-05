using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExplorableWorld
{
    class World
    {
        private string[,] Grid;
        private int Rows;
        private int Cols;

        public World(string[,] grid)
        {
            Grid= grid;
            Rows= grid.GetLength(0);
            Cols= grid.GetLength(1);
        }

        // y = row, x = col
        public void Draw()
        {
            string prompt = @"
   _____                        _______ _                
  / ____|                      |__   __(_)               
 | |  __  __ _ _ __ ___   ___     | |   _ _ __ ___   ___ 
 | | |_ |/ _` | '_ ` _ \ / _ \    | |  | | '_ ` _ \ / _ \
 | |__| | (_| | | | | | |  __/    | |  | | | | | | |  __/
  \_____|\__,_|_| |_| |_|\___|    |_|  |_|_| |_| |_|\___|
                                                      
(Use arrows to move. 
Press enter to place your ships then attack your opponent.)"; //prints out this ascii art header
            ForegroundColor = ConsoleColor.Magenta; //colour is set here
            WriteLine(prompt);
            ResetColor();

            WriteLine();
            WriteLine("     Your Ships                             Ai Ships");
            WriteLine();
            WriteLine("  A B C D E F G H                       A B C D E F G H"); // a tad lazy method but it allows for easier naviagtion?

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    string element = Grid[y, x];
                    SetCursorPosition(2*x + 2, y + 14);

                    if (element == "X")
                    {
                        ForegroundColor = ConsoleColor.Red;
                    }
                    else if (element == "@")
                    {
                        ForegroundColor= ConsoleColor.Green;
                    }
                    else if (element == "~")
                    {
                        ForegroundColor= ConsoleColor.Blue;
                    }
                    else if (element == "#")
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (element == "?")
                    {
                        ForegroundColor = ConsoleColor.Magenta;
                    }
                    Write($"{element}");
                }
                ResetColor();
                Write($" {y + 1}");
            }

        }

        public string GetElementAt(int x, int y)
        {
            return Grid[y, x];  
        }

        public bool IsPositionWalkable(int x, int y)
        {
            // Check bounds first.
            if (x < 0 || y < 0 || x >= Cols || y >= Rows)
            {
                return false;
            }

            // Check if the grid is a walkable position
            return Grid[y, x] == "~" || Grid[y, x] == "X" || Grid[y, x] == "#" || Grid[y, x] == "@" || Grid[y, x] == "-";
        }
    }
}
