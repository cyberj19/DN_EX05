using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game.Player.Algorithm;

namespace C17_Ex05.Game.Player
{
    class ComputerLogic
    {
        private ReverseTicTacToeAlgo m_Algorithm;
        private bool m_IsFirstMakeMove = true;

        public void init(GameBoardCell.eType i_Sign, GameLogic i_GameLogic)
        {
            m_Algorithm = new ReverseTicTacToeAlgo(i_Sign, i_GameLogic.State, i_GameLogic);
        }

        public Point? MakeMove(Board<GameBoardCell> i_Board, GameBoardCell.eType i_CellType, GameLogic i_GameLogic)
        {
            if (m_IsFirstMakeMove)
            {
                m_IsFirstMakeMove = false;
                init(i_CellType, i_GameLogic);
            }

            return m_Algorithm.GetMove();
        }
    }
}
