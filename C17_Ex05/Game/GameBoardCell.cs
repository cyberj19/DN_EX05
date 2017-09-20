namespace C17_Ex05.Game
{
    // Represents a cell in the Board
    struct GameBoardCell
    {
        public enum eType
        {
            None,
            X,
            O
        }

        private const string k_StringO = "O";
        private const string k_StringX = "X";
        private const string k_StringNone = " ";
        private readonly eType r_Type;
        
        public eType Type
        {
            get
            {
                return r_Type;
            }
        }

        public GameBoardCell(eType i_Type)
        {
            r_Type = i_Type;
        }

        public override string ToString()
        {
            string ret;

            switch(r_Type)
            {
                case eType.O:
                    ret = k_StringO;
                    break;
                case eType.X:
                    ret = k_StringX;
                    break;
                case eType.None:
                default:
                    ret = k_StringNone;
                    break;
            }

            return ret;
        }
    }
}
