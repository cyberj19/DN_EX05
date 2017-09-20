using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Game.Player
{
    class GamePlayer
    {
        public enum eType
        {
            HumanPlayer,
            ComputerPlayer
        }

        private readonly eType r_Type;
        private readonly GameBoardCell.eType r_CellType;
        private uint m_Score;
        private HumanLogic m_HumanLogic = null;
        private ComputerLogic m_ComputerLogic = null;

        public eType Type
        {
            get
            {
                return r_Type;
            }
        }

        public uint Score
        {
            get
            {
                return m_Score;
            }
        }

        public GameBoardCell.eType CellType
        {
            get
            {
                return r_CellType;
            }
        }

        public GamePlayer(eType i_Type, GameBoardCell.eType i_CellType)
        {
            m_Score = 0;
            r_Type = i_Type;
            r_CellType = i_CellType;
        }

        public void ResetLogicState()
        {
            switch (r_Type)
            {
                case eType.HumanPlayer:
                    m_HumanLogic = new HumanLogic();
                    break;
                case eType.ComputerPlayer:
                    m_ComputerLogic = new ComputerLogic();
                    break;
            }
        }

        public void AddScore()
        {
            m_Score++;
        }

        // is input required for the next make move request
        public bool IsInputRequiredForMove()
        {
            bool isInputRequired;

            switch (r_Type)
            {
                case eType.HumanPlayer:
                    isInputRequired = true;
                    break;
                case eType.ComputerPlayer:
                    isInputRequired = false;
                    break;
                default:
                    isInputRequired = false;
                    break;
            }

            return isInputRequired;
        }

        // Generate a GameBoardCell according to player's cell type
        public GameBoardCell GenereateCell()
        {
            return new GameBoardCell(r_CellType);
        }

        // Make a game move
        public Point? MakeMove(Board<GameBoardCell> i_Board, GameLogic i_GameLogic, Point? i_Input)
        {
            Point? retMove;

            if (IsInputRequiredForMove() && (!i_Input.HasValue))
            {
                retMove = null;
            }
            else
            {
                switch (r_Type)
                {
                    case eType.ComputerPlayer:
                        // forbidden return null as a computer move! as it should not get any input (An exception will be thrown)
                        retMove = (Point)m_ComputerLogic.MakeMove(i_Board, r_CellType, i_GameLogic);
                        break;
                    case eType.HumanPlayer:
                        retMove = m_HumanLogic.MakeMove(i_Board, (Point)i_Input, r_CellType, i_GameLogic);
                        break;
                    default:
                        retMove = null;
                        break;
                }
            }

            return retMove;
        }
    }
}
