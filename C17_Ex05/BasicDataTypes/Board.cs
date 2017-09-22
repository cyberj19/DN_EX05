namespace C17_Ex05.BasicDataTypes
{
    //todo: is good name
    internal delegate void BoardCellSetEventHandler(Point i_Pos, string i_ValueStr);

    // Represents a board with type 'T' Cells
    internal class Board<T>
    {
        private readonly T[,] r_Cells;
        private readonly uint r_NumRows;
        private readonly uint r_NumCols;
        internal event BoardCellSetEventHandler BoardCellSet;

        public uint Rows
        {
            get
            {
                return r_NumRows;
            }
        }

        public uint Cols
        {
            get
            {
                return r_NumCols;
            }
        }
        
        public Board(uint i_NumRows, uint i_NumCols)
        {
            r_NumRows = i_NumRows;
            r_NumCols = i_NumCols;
            r_Cells = new T[i_NumRows, i_NumCols];
        }

        // check if position is in bounds of the board
        public bool IsInBounds(Point i_Pos)
        {
            return (i_Pos.X < r_NumCols) && (i_Pos.Y < r_NumRows);
        }

        // Set cell of specific index
        public void Set(Point i_Pos, T i_NewCell)
        {
            r_Cells[i_Pos.Y, i_Pos.X] = i_NewCell;
            OnBoardCellSet(i_Pos, i_NewCell.ToString()); //todo : name..
        }

        protected virtual void OnBoardCellSet(Point i_Pos, string i_ValueStr)
        {
            if (BoardCellSet != null)
            {
                BoardCellSet.Invoke(i_Pos, i_ValueStr);
            }
        }

        // Get cell at specific index
        public T Get(Point i_Pos)
        {
            return r_Cells[i_Pos.Y, i_Pos.X];
        }
    }
}