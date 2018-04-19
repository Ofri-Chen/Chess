using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public static class Parser
    {
        public static Point ParsePositon(string pos)
        {
            if (pos?.Length != 2)
            {
                throw new ArgumentException("pos argument must be a string of 2 characters");
            }

            var isXParseSuccessful = int.TryParse(((char)(pos[0] - 49)).ToString(), out int xPos);
            var isYParseSuccessful = int.TryParse(pos[1].ToString(), out int yPos);
            yPos -= 1; //chess board's Y values are from 1 to 8 


            if ((!isXParseSuccessful || !isYParseSuccessful) ||
                !(xPos >= 0 && xPos <= 7) ||
                !(yPos >= 0 && yPos <= 7))
            {
                throw new ArgumentException("invalid input");
            }

            return new Point(xPos, yPos);
        }        
    }
}
