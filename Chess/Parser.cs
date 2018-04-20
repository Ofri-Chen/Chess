using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using static Chess.Program;

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

        #region BuildBoard

        public static Piece[][] CreateBoard(Piece[][] board, string boardStr)
        {
            for (int i = 0; i < Configuration.ROWS; i++)
            {
                for (int j = 0; j < Configuration.COLS; j++)
                {
                    board[i][j] = CreatePiece(boardStr[i * 8 + j], new Point(j, i));
                }
            }

            return board;
        }

        private static Piece CreatePiece(char pieceValue, Point pos)
        {
            if (pieceValue == '#') return null; //empty square
            if (!pieceValue.IsEnglishLetter())
            {
                throw new Exception("invalid pieceValue");
            }

            var pieceType = Configuration.PieceTypeMapping[pieceValue.ToLower()];
            var player = pieceValue.IsUpperCase() ? PlayerTypes.Black : PlayerTypes.White;

            return (Piece)Activator.CreateInstance(pieceType, pos, player);
        }

        #endregion
    }
}
