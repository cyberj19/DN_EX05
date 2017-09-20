namespace C17_Ex05.Game
{
    struct GameResult
    {
        public enum eResult
        {
            Draw,
            PlayerWon
        }

        private readonly eResult r_Result;
        private readonly uint? r_WinPlayerIndex;

        public eResult Result
        {
            get
            {
                return r_Result;
            }
        }

        public uint WinPlayerIndex
        {
            get
            {
                // if this wasnt set, an exception will be thrown
                return (uint)r_WinPlayerIndex;
            }
        }

        public GameResult(eResult i_Result)
        {
            r_Result = i_Result;
            r_WinPlayerIndex = null;
        }

        public GameResult(eResult i_Result, uint i_WinPlayerIndex)
        {
            r_Result = i_Result;
            r_WinPlayerIndex = i_WinPlayerIndex;
        }
    }
}
