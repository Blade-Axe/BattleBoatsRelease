using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExplorableWorld
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        private string PlayerMarker;
        private ConsoleColor PlayerColor;
        public Player(int initialX, int initialY) 
        { 
            X = initialX;
            Y = initialY;

            PlayerMarker = "+"; //Is the icon that is displayed
            
            PlayerColor = ConsoleColor.Yellow; //colour of icon
        }
        
        public void Draw(int boatcount) //gets boatcount from list of public variables and checks if its less than 5,
                                        //and if so set the cursor to player board, if its larger move cursor to ai board.
        {
            if (boatcount < 5)
            {
                ForegroundColor = PlayerColor;
                SetCursorPosition(2 * X + 2, Y + 14);
                Write(PlayerMarker);
                ResetColor();
            }
            else
            {
                ForegroundColor = PlayerColor;
                SetCursorPosition(2 * X + 40, Y + 14);
                Write(PlayerMarker);
                ResetColor();
            }
        }
    }
}
