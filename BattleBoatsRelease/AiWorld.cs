using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExplorableWorld
{
    class AiWorld
    {
        private string[,] Grid;
        private int Rows;
        private int Cols;
        public bool showboats;

        public AiWorld(string[,] aigrid)
        {
            Grid = aigrid;
            Rows = aigrid.GetLength(0);
            Cols = aigrid.GetLength(1);
        }

        // y = row, x = col
        public void Draw(bool boattoggle)
        {
            showboats = boattoggle; //allows for the visibility of boats to be set in the program via the settings page

            for (int y = 0; y < Rows; y++)
            {

                for (int x = 0; x < Cols; x++) //loops through and draws the grid
                {
                    string element = Grid[y, x];
                    SetCursorPosition(2 * x + 40, y + 14); //sets spacing for the grid aka [~ ~ ~ ~]

                    if (element == "X") //checks for elements in the grid while its drawing and applies a colour
                    {
                        ForegroundColor = ConsoleColor.Red;
                    }

                    else if (element == "@")
                    {

                        ForegroundColor = ConsoleColor.Green;
                    }
                    else if (element == "~")
                    {
                        ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (element == "#")
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (element == "?")
                    {
                        ForegroundColor = ConsoleColor.Magenta;
                    }

                    while (showboats == false) //allows for the ais boats to be hidden
                    {
                        if (element != "@") //if its not the boat symbol "@" then write it to the screen
                        {
                            Write($"{element}");
                        }
                        else //if it is the "@" then hide it by writing a "~" over it and colouring it blue
                        {
                            ForegroundColor = ConsoleColor.Blue;
                            Write("~");
                            ResetColor();
                        }
                        break;
                    }
                    if (showboats == true) //just writes the board without checking for the "@" symbol
                    {
                        Write($"{element}");
                    }
                   

                }
                ResetColor();
                Write($" {y + 1}"); //adds the numbers to the right hand side of the column
            }
        }

        public string GetElementAt(int x, int y) //gets the element at a specified set of coordinates
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
            return Grid[y, x] == "~" || Grid[y, x] == "X" || Grid[y, x] == "#" || Grid[y, x] == "@";
        }
    }
}

