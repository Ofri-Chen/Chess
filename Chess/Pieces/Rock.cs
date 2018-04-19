using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Rock : Piece
    {
        public Rock(string pos, PlayerTypes player) : base(pos, player) { }
        public Rock(Point pos, PlayerTypes player) : base(pos, player) { }

        public override IEnumerable<Point> GetPossibleMoves(Point[][] board)
        {
            throw new NotImplementedException();
        }
    }
}
