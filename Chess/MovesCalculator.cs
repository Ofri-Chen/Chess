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
            return GetLinePossibleMoves(piece, board, 1, 0)
                .Concat(GetLinePossibleMoves(piece, board, -1, 0))
                .Concat(GetLinePossibleMoves(piece, board, 0, 1))
                .Concat(GetLinePossibleMoves(piece, board, 0, -1))
                .ToArray();
        }
        public static Point[] DiagonalLine(Piece piece, Piece[][] board)
        {
            return GetLinePossibleMoves(piece, board, 1, 1)
                .Concat(GetLinePossibleMoves(piece, board, -1, 1))
                .Concat(GetLinePossibleMoves(piece, board, -1, -1))
                .Concat(GetLinePossibleMoves(piece, board, 1, -1))
                .ToArray();
        }
        public static Point[] OneSquareToAnyDirection(Piece piece, Piece[][] board)
        {
            var pos = piece.Position;

            var possibleSquares = GetSurroundingPositions(new Point(pos.X, pos.Y + 1), 1, 0)
                .Concat(GetSurroundingPositions(new Point(pos.X, pos.Y), 1, 0))
                .Concat(GetSurroundingPositions(new Point(pos.X, pos.Y - 1), 1, 0))
                .Concat(GetSurroundingPositions(new Point(pos.X, pos.Y), 0, 1));

            return possibleSquares.Where(square => board.At(square)?.Player != piece.Player).ToArray();
        }

        private static IEnumerable<Point> GetSurroundingPositions(Point point, int xIncrementor, int yIncrementor)
        {
            var points = new Point[]
            {
                new Point(point.X + xIncrementor, point.Y + yIncrementor),
                new Point(point.X - xIncrementor, point.Y - yIncrementor)
            };

            return points.Where(surroundingPoint => ChessUtils.IsValidPosition(surroundingPoint));
        }


        private static IEnumerable<Point> GetLinePossibleMoves(Piece piece, Piece[][] board,
            int loopXIncrementor, int loopYIncrementor)
        {
            ICollection<Point> possibleMoves = new List<Point>();

            if (loopXIncrementor == 0 && loopYIncrementor == 0)
            {
                throw new ArgumentException();
            }

            var iterator = 1;

            while (ChessUtils.IsValidPosition(piece.Position.X + loopXIncrementor * iterator,
                                  piece.Position.Y + loopYIncrementor * iterator))
            {
                var xPos = piece.Position.X + loopXIncrementor * iterator;
                var yPos = piece.Position.Y + loopYIncrementor * iterator;

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

                iterator++;
            }
            return possibleMoves;
        }
    }
}