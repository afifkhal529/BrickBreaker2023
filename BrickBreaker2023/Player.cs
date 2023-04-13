using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker2023
{
    internal class Player
    {
        public int x, y;
        public int speed = 6;
        public int width = 20;
        public int height = 10;

        public Player(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Move(string direction)
        {
            if (direction == "right")
            {
                x += speed;
            }

            if (direction == "left")
            {
                x -= speed;
            }
        }
    }
}
