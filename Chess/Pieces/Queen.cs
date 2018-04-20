using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(string pos, PlayerTypes player) : base(pos, player) { }
        public Queen(Point pos, PlayerTypes player) : base(pos, player) { }

        public override Point[] GetPossibleMoves(Piece[][] board)
        {
            return MovesCalculator.CalculateMoves(this, board, MovesCalculator.StraightLine, MovesCalculator.DiagonalLine);
        }
    }
}
