using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public static class MovesCalculator
    {
        public static Point[] StraightLine(Piece piece, Piece[][] board)
        {
            return GetStraightLinePossibleMovesOnDirection(piece, board, 1, 0)
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, -1, 0))
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, 0, 1))
                .Concat(GetStraightLinePossibleMovesOnDirection(piece, board, 0, -1))
                .ToArray();
        }

        private static IEnumerable<Point> GetStraightLinePossibleMovesOnDirection(Piece piece, Piece[][] board, 
            int loopXIncrementor, int loopYIncrementor)
        {
            ICollection<Point> possibleMoves = new List<Point>();

            if(loopXIncrementor == 0 && loopYIncrementor == 0)
            {
                throw new ArgumentException();
            }

            int numOfIterations = GetStraightLineNumOfIterations(piece, loopXIncrementor, loopYIncrementor);

            for (int i = 1; i < numOfIterations; i++)
            {
                if (board[piece.Position.Y + loopYIncrementor][piece.Position.X + loopXIncrementor] == null)
                {
                    possibleMoves.Add(new Point(piece.Position.X + i, piece.Position.Y));
                }
                else
                {
                    if (board[piece.Position.Y][piece.Position.X + i].Player != piece.Player)
                    {
                        possibleMoves.Add(new Point(piece.Position.X + i, piece.Position.Y));
                        break;
                    }
                }
            }

            return possibleMoves;
        }

        private static int GetStraightLineNumOfIterations(Piece piece, int loopXIncrementor, int loopYIncrementor)
        {
            if(loopXIncrementor > 0)
            {
                return BoardManager.COLS - piece.Position.X;
            }
            if(loopXIncrementor < 0)
            {
                return piece.Position.X;
            }
            if (loopYIncrementor > 0)
            {
                return BoardManager.ROWS - piece.Position.X;
            }
            if (loopYIncrementor < 0)
            {
                return piece.Position.Y;
            }

            return 0;
        }
    }
}
