using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(string pos, PlayerTypes player) : base(pos, player) { }
        public Bishop(Point pos, PlayerTypes player) : base(pos, player) { }

        public override Point[] GetPossibleMoves(Piece[][] board)
        {
            return MovesCalculator.CalculateMoves(this, board, MovesCalculator.DiagonalLine);
        }
    }
}
