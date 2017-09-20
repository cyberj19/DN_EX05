using C17_Ex05.Game;

namespace C17_Ex05.UI
{
    // holds the initial params for the program
    class ReversedTicTacToeParams
    {
        private GameType m_GameType;
        private uint m_BoardSize;

        public GameType GameType
        {
            get
            {
                return m_GameType;
            }
        }

        public uint BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public ReversedTicTacToeParams(GameType i_GameType, uint i_BoardSize)
        {
            m_GameType = i_GameType;
            m_BoardSize = i_BoardSize;
        }
    }
}
