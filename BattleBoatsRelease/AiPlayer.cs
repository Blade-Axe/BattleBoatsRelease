using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ExplorableWorld
{
    internal class AiPlayer
    {
        public int X { get; set; }
        public int Y { get; set; }

        public AiPlayer(int initialX, int initialY)
        {
            Random randomX1 = new Random();
            Random randomY1 = new Random();

            initialX = randomX1.Next(7);
            initialY = randomY1.Next(7);

            X = initialX;
            Y = initialY;
        }

        public void Draw()
        {
            SetCursorPosition(2 * X + 40, Y + 14); //sets position of coordinate on grid
        }

        public int Next_X()
        {
            Random randomX1 = new Random(); //generates a random number for the X coordinate
            return randomX1.Next(0,8);
        }
        public int Next_Y()
        {
            Random randomY1 = new Random(); //generate a random number for the Y coordinate
            return randomY1.Next(0,8);
        }
    }
}
