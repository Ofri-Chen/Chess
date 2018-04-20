using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(string pos, PlayerTypes player) : base(pos, player) { }
        public Knight(Point pos, PlayerTypes player) : base(pos, player) { }

        public override Point[] GetPossibleMoves(Piece[][] board)
        {
            throw new NotImplementedException();
        }
    }
}
