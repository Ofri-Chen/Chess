using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using static Chess.Program;

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
        public static Point[] Knight(Piece piece, Piece[][] board)
        {
            var pos = piece.Position;

            var possibleSquares = GetSurroundingPositions(new Point(pos.X, pos.Y + 2), 1, 0)
                .Concat(GetSurroundingPositions(new Point(pos.X, pos.Y - 2), 1, 0))
                .Concat(GetSurroundingPositions(new Point(pos.X + 2, pos.Y), 0, 1))
                .Concat(GetSurroundingPositions(new Point(pos.X - 2, pos.Y), 0, 1));

            return possibleSquares.Where(square => board.At(square)?.Player != piece.Player).ToArray();
        }
        public static Point[] Pioneer(Piece piece, Piece[][] board)
        {
            var piecePos = piece.Position;
            ICollection<Point> possibleSquares = new List<Point>();
            int playerModifier = piece.Player == PlayerTypes.White ? 1 : -1;


            var tempPos = new Point(piecePos.X, piecePos.Y + 1 * playerModifier);
            if (tempPos.IsValidPosition())
            {
                if (board.At(tempPos) == null)
                {
                    possibleSquares.Add(tempPos);
                    tempPos = new Point(piecePos.X, piecePos.Y + 2 * playerModifier);
                    if ((tempPos.IsValidPosition() && (board.At(tempPos) == null)) &&
                        ((piecePos.Y == 1 && piece.Player == PlayerTypes.White) || (piecePos.Y == 6 && piece.Player == PlayerTypes.Black)))
                    {
                        possibleSquares.Add(tempPos);
                    }
                }

                tempPos = new Point(piecePos.X + 1, piecePos.Y + 1 * playerModifier);
                if (tempPos.IsValidPosition() && board.At(tempPos).Player == piece.Player.GetOtherPlayer())
                {
                    possibleSquares.Add(tempPos);
                }
                tempPos = new Point(piecePos.X - 1, piecePos.Y + 1 * playerModifier);
                if (tempPos.IsValidPosition() &&  board.At(tempPos)?.Player == piece.Player.GetOtherPlayer())
                {
                    possibleSquares.Add(tempPos);
                }
            }

            return possibleSquares.ToArray();
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