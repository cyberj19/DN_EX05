using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Game.Player
{
    class HumanLogic
    {
        // Makes a move. If bad input was recv, will return null.
        public Point? MakeMove(Board<GameBoardCell> i_Board, Point i_Input, GameBoardCell.eType i_CellType, GameLogic i_GameLogic)
        {
            Point? retMove = null;

            // checking if move is valid both here and in "GameManager". If the move is not valid in this stage, its the user's input fault.
            // If the move is not valid in "GameManager" Then its an error!
            if (i_GameLogic.IsMoveValid(i_Input))
            {
                retMove = i_Input;
            }

            return retMove;
        }
    }
}
