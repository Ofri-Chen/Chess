using Core;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        public Point Position { get; set; }
        public PlayerTypes Player { get; }

        public Piece(string pos, PlayerTypes player)
        {
            Position = Parser.ParsePositon(pos);
            Player = player;
        }

        public Piece(Point pos, PlayerTypes player)
        {
            Position = pos;
            Player = player;
        }

        public abstract IEnumerable<Point> GetPossibleMoves(Point[][] board);
    }
}
