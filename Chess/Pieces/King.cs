using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(string pos, PlayerTypes player) : base(pos, player) { }
        public King(Point pos, PlayerTypes player) : base(pos, player) { }

        public override Point[] GetPossibleMoves(Piece[][] board)
        {
            throw new NotImplementedException();
        }
    }
}
