using Chess.Pieces;
using System;
using System.Collections.Generic;

namespace Chess
{
    partial class Program
    {
        public static class Configuration
        {
            public static readonly int ROWS = 8;
            public static readonly int COLS = 8;

            public static Dictionary<char, Type> PieceTypeMapping = new Dictionary<char, Type>
            {
                {'k', typeof(King) },
                {'q', typeof(Queen) },
                {'r', typeof(Rock) },
                {'n', typeof(Knight) },
                {'b', typeof(Bishop) },
                {'p', typeof(Pioneer) }
            };
        }
    }
}