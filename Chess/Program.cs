using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Chess.Pieces;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardStr = "qkrrnnbbPPPPPPPP################################ppppppppQKRRNNBB0";

            var startingPlayer = Utils.TryParseToEnum<PlayerTypes>(int.Parse(boardStr[64].ToString()));

            var a = new string(boardStr.Take(BoardManager.ROWS * BoardManager.COLS).ToArray());
            var boardManager = new BoardManager(a);
            boardManager.Print();
        }
    }
}