using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Chess.Pieces;
using System.Collections.Generic;
using System;

namespace Chess
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //var boardStr = "qkrrnnbbPPPPPPPP################################ppppppppQKRRNNBB0";
            var boardStr = "#####k#############rQKr#########################################0";

            var startingPlayer = Utils.TryParseToEnum<PlayerTypes>(int.Parse(boardStr[64].ToString()));

            var a = new string(boardStr.Take(Configuration.ROWS * Configuration.COLS).ToArray());
            var gameManager = new GameManager(a);

            var res = gameManager.MovePiece("d3e3");
        }
    }
}