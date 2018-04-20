using System;
using Core;
using Chess.Pieces;
using System.Linq;
using System.Collections.Generic;

namespace Chess
{
    partial class Program
    {
        public class GameManager
        {
            public Piece[][] Board { get; private set; }
            private PlayerTypes CurrPlayer { get; set; }
            private List<FuncEnumPair<Point, Statuses>> _validations;

            public GameManager(string boardStr)
            {
                InitBoard();
                InitValidations();
                Board = Parser.CreateBoard(Board, boardStr);
                PrintBoard();
            }

            public Statuses MovePiece(string moveStr)
            {
                ParseMoveStr(moveStr, out Point origin, out Point dest);

                var movementStatus = _validations.First(funcEnumPair => funcEnumPair.Func(origin, dest)).Value;
                if (movementStatus != Statuses.Valid) return movementStatus;

                if (!TryPerformMove(origin, dest)) return Statuses.Invalid_SelfCheck;

                Console.Clear();
                PrintBoard();

                var otherPlayer = GetOtherPlayer(CurrPlayer);
                var rows = Board.SelectMany(row => row);

                var otherPlayerPiecesMovesDic = rows.Where(piece => piece?.Player == otherPlayer)
                    .ToDictionary(piece => piece, piece => piece.GetPossibleMoves(Board));
                var possibleMoves = GetPossibleMoves(rows, CurrPlayer);


                if (IsPlayerChecked(otherPlayer, rows, possibleMoves))
                {
                    return IsPlayerCheckmated(otherPlayer, rows, otherPlayerPiecesMovesDic)
                        ? Statuses.Valid_Checkmate
                        : Statuses.Valid_Check;
                }

                return Statuses.Valid;
            }

            private bool TryPerformMove(Point origin, Point dest)
            {
                Piece destPiece = Board.At(dest); //to restore the old board
                ChangePiecePos(Board.At(origin), dest);
                Board[origin.Y][origin.X] = null;

                var rows = Board.SelectMany(row => row);

                if (IsPlayerChecked(CurrPlayer, rows))
                {
                    ChangePiecePos(Board.At(dest), origin);
                    Board[dest.Y][dest.X] = destPiece;

                    return false;
                }

                return true;
            }

            private bool IsPlayerChecked(PlayerTypes player, IEnumerable<Piece> rows, IEnumerable<Point> enemyPossibleMoves = null)
            {
                var enemy = GetOtherPlayer(player);
                enemyPossibleMoves = enemyPossibleMoves ?? GetPossibleMoves(rows, enemy);
                var kingPos = rows.First(piece => (piece?.GetType() == typeof(King) && piece?.Player == player)).Position;

                return enemyPossibleMoves.Any(possibleMove => possibleMove.Equals(kingPos));
            }

            public bool IsPlayerCheckmated(PlayerTypes player, IEnumerable<Piece> rows,
                Dictionary<Piece, Point[]> piecesMovesDic)
            {
                foreach (var piece in piecesMovesDic)
                {
                    foreach (var newPos in piece.Value)
                    {
                        //Simulate board after move
                        var oldPos = piece.Key.Position;
                        Piece destPiece = Board.At(newPos); //To restore the old board
                        ChangePiecePos(Board.At(oldPos), newPos);
                        Board[oldPos.Y][oldPos.X] = null;

                        var enemyPossibleMoves = GetPossibleMoves(rows, CurrPlayer);
                        var isPlayerChecked = IsPlayerChecked(player, rows, enemyPossibleMoves);

                        //Restore old board
                        ChangePiecePos(Board.At(newPos), oldPos);
                        Board[newPos.Y][newPos.X] = destPiece;

                        if (!isPlayerChecked) return false;
                    }
                }

                return true;
            }

            #region Initialize
            private void InitBoard()
            {
                Board = new Piece[Configuration.ROWS][];
                for (int i = 0; i < Configuration.ROWS; i++)
                {
                    Board[i] = new Piece[Configuration.COLS];
                }
            }

            private void InitValidations()
            {
                _validations = new List<FuncEnumPair<Point, Statuses>>
                {
                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                        !(ChessUtils.IsValidPosition(origin) && ChessUtils.IsValidPosition(origin)),
                        Statuses.Invalid_Indexes),

                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                        Board.At(origin)?.Player != CurrPlayer,
                        Statuses.Invalid_OriginIsNotCurrentPlayerPiece),

                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                       Board.At(dest)?.Player == CurrPlayer,
                       Statuses.Invalid_DestIsCurrentPlayerPiece),

                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                       origin.Equals(dest),
                       Statuses.Invalid_OriginIsEqualToDest),

                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                        !Board.At(origin).GetPossibleMoves(Board).Contains(dest),
                        Statuses.Invalid_PieceMovement),

                    new FuncEnumPair<Point, Statuses>((origin, dest) =>
                        Board.At(origin).GetPossibleMoves(Board).Contains(dest),
                        Statuses.Valid)
                };
            }
            #endregion

            #region Assistance Methods
            private void ParseMoveStr(string moveStr, out Point origin, out Point dest)
            {
                if (moveStr.Length != 4)
                {
                    throw new ArgumentException("moveStr must have the length of 4");
                }

                origin = Parser.ParsePositon(moveStr.Substring(0, 2));
                dest = Parser.ParsePositon(moveStr.Substring(2, 2));
            }
            private void ChangePiecePos(Piece piece, Point newPos)
            {
                Board[newPos.Y][newPos.X] = piece;
                piece.Position = newPos;
            }
            private static PlayerTypes GetOtherPlayer(PlayerTypes player)
            {
                return (((int)player) ^ 1).TryParseToEnum<PlayerTypes>();
            }
            private Point[] GetPossibleMoves(IEnumerable<Piece> rows, PlayerTypes player)
            {
                return rows.Where(piece => piece?.Player == player)
                    .SelectMany(piece => piece.GetPossibleMoves(Board))
                    .ToArray();
            }
            #endregion

            public void PrintBoard()
            {
                for (int i = 0; i < Configuration.ROWS; i++)
                {
                    for (int j = 0; j < Configuration.COLS; j++)
                    {
                        string name;
                        var piece = Board[i][j];
                        if (piece != null)
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

        }
    }
}