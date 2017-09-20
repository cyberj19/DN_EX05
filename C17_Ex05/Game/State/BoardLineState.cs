namespace C17_Ex05.Game.State
{
    // Represents that state of a single board line (Will speed up checking if the game is over
    // and will help Algorithm-implementors)
    struct BoardLineState
    {
        public enum eLineType
        {
            Row,
            Col,
            Diagonal,
            AntiDiagonal
        }

        private readonly eLineType r_LineType;
        private readonly uint[] r_AmountOfCellsByPlayer;
        private readonly uint r_TotalNumberOfCells;
        private uint m_TotalAmountOfCellsUsed;

        public eLineType Type
        {
            get
            {
                return r_LineType;
            }
        }

        public BoardLineState(eLineType i_LineType, uint i_AmountOfPlayers, uint i_TotalLineLength)
        {
            r_LineType = i_LineType;
            r_AmountOfCellsByPlayer = new uint[i_AmountOfPlayers];
            m_TotalAmountOfCellsUsed = 0;
            r_TotalNumberOfCells = i_TotalLineLength;
        }

        // if atleast one 'set' was called on this line, find first player.
        // Otherwise, returns null
        public uint? GetFirstPlayerWithItemIndex()
        {
            uint? retIndex = null;

            for (uint i = 0; i < r_AmountOfCellsByPlayer.Length; i++)
            {
                if (r_AmountOfCellsByPlayer[i] > 0)
                {
                    retIndex = i;
                    break;
                }
            }

            return retIndex;
        }

        // Get the amount of cells in line of a speicifc player
        public uint GetAmountOfCellsUsedByPlayer(uint i_PlayerIndex)
        {
            return r_AmountOfCellsByPlayer[i_PlayerIndex];
        }

        // Is the line full
        public bool IsLineFull()
        {
            return r_TotalNumberOfCells == m_TotalAmountOfCellsUsed;
        }

        // Is the line full and all cells are of same player?
        public bool IsLineFullBySinglePlayer()
        {
            return IsLineFull() && (r_AmountOfCellsByPlayer[(uint)GetFirstPlayerWithItemIndex()] == r_TotalNumberOfCells);
        }

        // a 'set' was called for a cell in this line
        public void Set(uint i_PlayerIndex)
        {
            r_AmountOfCellsByPlayer[i_PlayerIndex]++;
            m_TotalAmountOfCellsUsed++;
        }
    }
}
