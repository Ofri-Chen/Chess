using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Pioneer : Piece
    {
        public Pioneer(string pos, PlayerTypes player) : base(pos, player) { }
        public Pioneer(Point pos, PlayerTypes player) : base(pos, player) { }

        public override IEnumerable<Point> GetPossibleMoves(Point[][] board)
        {
            throw new NotImplementedException();
        }
    }
}
