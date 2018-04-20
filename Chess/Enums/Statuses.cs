namespace Chess
{
    partial class Program
    {
        public enum Statuses
        {
            Valid = 0,
            Valid_Check = 1,
            Valid_Checkmate = 8,

            Invalid_OriginIsNotCurrentPlayerPiece = 2,
            Invalid_DestIsCurrentPlayerPiece = 3,
            Invalid_SelfCheck = 4,
            Invalid_Indexes = 5,
            Invalid_PieceMovement = 6,
            Invalid_OriginIsEqualToDest = 7
        }
    }
}