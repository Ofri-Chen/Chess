using Chess.Pieces;
using Core;
using System;
using System.Collections.Generic;

namespace Chess
{
    public class BoardManager
    {
        public static readonly int ROWS = 8;
        public static readonly int COLS = 8;

        public Piece[][] Board { get; private set; }

        public BoardManager(string boardStr)
        {
            CreateBoard(boardStr);
        }

        public void Print()
        {
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    string name;
                    var piece = Board[i][j];
                    if(piece != null)
                    {
                        var pieceName = piece.GetType().Name;
                        name = piece.Player == PlayerTypes.Black ? pieceName.ToUpper() : pieceName.ToLower();
                    }
                    else
                    {
                        name = "#";
                    }
                    Console.Write(name + ",");
                }
                Console.WriteLine();
            }
        }

        public void CreateBoard(string boardStr)
        {
            InitBoard();

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    Board[i][j] = CreatePiece(boardStr[i * 8 + j], new Point(i, j));
                }
            }
            
        }

        private void InitBoard()
        {
            Board = new Piece[ROWS][];
            for (int i = 0; i < ROWS; i++)
            {
               Board[i] = new Piece[COLS];
            }
        }

        private static Piece CreatePiece(char pieceValue, Point pos)
        {
            if (pieceValue == '#') return null; //empty square
            if (!pieceValue.IsEnglishLetter())
            {
                throw new Exception("invalid pieceValue");
            }

            var pieceType = _pieceTypeMapping[pieceValue.ToLower()];
            var player = pieceValue.IsUpperCase() ? PlayerTypes.Black : PlayerTypes.White;

            return (Piece)Activator.CreateInstance(pieceType, pos, player);
        }

        private static Dictionary<char, Type> _pieceTypeMapping = new Dictionary<char, Type>
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
