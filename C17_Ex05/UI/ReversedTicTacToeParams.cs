using C17_Ex05.Game;

namespace C17_Ex05.UI
{
    // holds the initial params for the program
    class ReversedTicTacToeParams
    {
        private readonly GameType r_GameType;
        private readonly uint r_BoardSize;
        private readonly string[] r_PlayerNames;

        public GameType GameType
        {
            get
            {
                return r_GameType;
            }
        }

        public uint BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public string[] PlayerNames
        {
            get
            {
                return r_PlayerNames;
            }
        }

        public ReversedTicTacToeParams(GameType i_GameType, uint i_BoardSize, string[] i_PlayerNames)
        {
            r_GameType = i_GameType;
            r_BoardSize = i_BoardSize;
            r_PlayerNames = i_PlayerNames;
        }
    }
}
