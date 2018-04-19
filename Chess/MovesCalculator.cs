using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public static class MovesCalculator
    {
        public static Point[] CalculateMoves(Piece piece, Piece[][] board, params Func<Piece, Piece[][], Point[]>[] funcs)
        {
            return funcs.SelectMany(func => func(piece, board)).ToArray();
        }

        public static Point[] StraightLine(Piece piece, Piece[][] board)
        {
            return GetStraightLinePossibleMovesOnDirection(piece, board, 1, 0)
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, -1, 0))
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, 0, 1))
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, 0, -1))
                .ToArray();
        }


        #region StraightLine
        private static IEnumerable<Point> GetStraightLinePossibleMovesOnDirection(Piece piece, Piece[][] board,
            int loopXIncrementor, int loopYIncrementor)
        {
            ICollection<Point> possibleMoves = new List<Point>();

            if (loopXIncrementor == 0 && loopYIncrementor == 0)
            {
                throw new ArgumentException();
            }

            int numOfIterations = GetStraightLineNumOfIterations(piece, loopXIncrementor, loopYIncrementor);

            for (int i = 1; i < numOfIterations + 1; i++)
            {
                var xPos = piece.Position.X + loopXIncrementor * i;
                var yPos = piece.Position.Y + loopYIncrementor * i;

                if (board[yPos][xPos] == null)
                {
                    possibleMoves.Add(new Point(xPos, yPos));
                }
                else
                {
                    if (board[yPos][xPos].Player != piece.Player)
                    {
                        possibleMoves.Add(new Point(xPos, yPos));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return possibleMoves;
        }

        private static int GetStraightLineNumOfIterations(Piece piece, int loopXIncrementor, int loopYIncrementor)
        {
            if (loopXIncrementor > 0)
            {
                return BoardManager.COLS - piece.Position.X - 1;
            }
            if (loopXIncrementor < 0)
            {
                return piece.Position.X;
            }
            if (loopYIncrementor > 0)
            {
                return BoardManager.ROWS - piece.Position.Y - 1;
            }
            if (loopYIncrementor < 0)
            {
                return piece.Position.Y;
            }

            return 0;
        }
        #endregion
    }
}
