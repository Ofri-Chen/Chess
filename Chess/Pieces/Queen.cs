using Core;
using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(string pos, PlayerTypes player) : base(pos, player) { }
        public Queen(Point pos, PlayerTypes player) : base(pos, player) { }

        public override IEnumerable<Point> GetPossibleMoves(Point[][] board)
        {
            throw new NotImplementedException();
        }
    }
}
