using System;
using System.Collections.Generic;
using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Game.State
{
    // Handles 'BoardLineStates' of all line of the board (Will speed up checking if the game is over
    // and will help Algorithm-implementors)
    class GameState
    {
        private const uint k_BasicPointNumLines = 2; // col + row
        private const int k_SizeToIndexDifference = 1;
        private const int k_ItemNotFound = -1;
        private readonly BoardLineState[] r_RowsState;
        private readonly BoardLineState[] r_ColsState;
        private readonly LinkedList<Point> r_FreePoints;
        private BoardLineState m_DiagonalState; // Can be changed, cannot be readonly
        private BoardLineState m_AntiDiagonalState; // Can be changed, cannot be readonly
        private uint m_TotalNumOfSetFunctionCalls;

        public LinkedList<Point> FreeBoardPoints
        {
            get
            {
                return r_FreePoints;
            }
        }

        public GameState(uint i_AmountOfPlayers, uint i_NumRows, uint i_NumCols)
        {
            uint diagonalSize = Math.Min(i_NumRows, i_NumCols);

            m_TotalNumOfSetFunctionCalls = 0;
            m_DiagonalState = new BoardLineState(BoardLineState.eLineType.Diagonal, i_AmountOfPlayers, diagonalSize);
            m_AntiDiagonalState = new BoardLineState(BoardLineState.eLineType.AntiDiagonal, i_AmountOfPlayers, diagonalSize);
            r_RowsState = createStateArray(BoardLineState.eLineType.Row, i_AmountOfPlayers, i_NumCols, i_NumRows);
            r_ColsState = createStateArray(BoardLineState.eLineType.Col, i_AmountOfPlayers, i_NumRows, i_NumCols);
            r_FreePoints = createBoardPointsLinkedList(i_NumRows, i_NumCols);
        }

        // If there's a player that has a full line of it's cells - returns player index.
        // otherwise - null.
        // The player index must be present in 'i_ValidPlayersIndices', otherwise we will ignore it.
        // The line to be searched is a line that 'i_Pos' is inside of it.
        public uint? GetPlayerIfPointInFullLine(Point i_Pos, List<uint> i_ValidPlayersIndices)
        {
            uint? playerIndex = null;
            List<BoardLineState> pointLines = GetStateLinesOfPoint(i_Pos);

            foreach (BoardLineState line in pointLines)
            {
                if (line.IsLineFullBySinglePlayer())
                {
                    playerIndex = line.GetFirstPlayerWithItemIndex();
                    if (i_ValidPlayersIndices.IndexOf((uint)playerIndex) == k_ItemNotFound)
                    {
                        playerIndex = null;
                    }
                    else
                    {
                        break;
                    }                    
                }
            }

            return playerIndex;               
        }

        // Get lines of a specific cell
        public List<BoardLineState> GetStateLinesOfPoint(Point i_Pos)
        {
            uint numOfLines = getNumLinesOfPoint(i_Pos);
            List<BoardLineState> retLines = new List<BoardLineState>((int)numOfLines);

            retLines.Add(r_RowsState[i_Pos.Y]);
            retLines.Add(r_ColsState[i_Pos.X]);
            if (IsInDiagonal(i_Pos))
            {
                retLines.Add(m_DiagonalState);
            }

            if (IsInAntiDiagonal(i_Pos))
            {
                retLines.Add(m_AntiDiagonalState);
            }

            return retLines;
        }

        // Get number of lines a point is in
        private uint getNumLinesOfPoint(Point i_Pos)
        {
            uint numLines = k_BasicPointNumLines;

            if (IsInAntiDiagonal(i_Pos))
            {
                numLines++;
            }

            if (IsInDiagonal(i_Pos))
            {
                numLines++;
            }

            return numLines;
        }

        // Is cell in the diagonal
        public bool IsInDiagonal(Point i_Pos)
        {
            return i_Pos.X == i_Pos.Y;
        }

        // Is cell in the anti diagonal
        public bool IsInAntiDiagonal(Point i_Pos)
        {
            return i_Pos.Y == (r_ColsState.Length - k_SizeToIndexDifference - i_Pos.X);
        }

        // Create points linked list (Represents empty points linked list in a board)
        private LinkedList<Point> createBoardPointsLinkedList(uint i_NumRows, uint i_NumCols)
        {
            LinkedList<Point> retPointsList = new LinkedList<Point>();

            for (uint currRow = 0; currRow < i_NumRows; currRow++)
            {
                for (uint currCol = 0; currCol < i_NumCols; currCol++)
                {
                    retPointsList.AddLast(new Point(currCol, currRow));
                }
            }

            return retPointsList;
        }

        // Delete a point from the list of empty cells
        private void deletePointFromFreePointsLinkedList(Point i_Pos)
        {
            r_FreePoints.Remove(i_Pos);
        }

        // 'Set' a cell in our state (Update all relevant lines of a cell)
        public void Set(Point i_Pos, uint i_PlayerIndex)
        {
            m_TotalNumOfSetFunctionCalls++;
            deletePointFromFreePointsLinkedList(i_Pos); // Point is no longer empty
            r_RowsState[i_Pos.Y].Set(i_PlayerIndex); // update stae of relevant lines state
            r_ColsState[i_Pos.X].Set(i_PlayerIndex);
            if (IsInDiagonal(i_Pos))
            {
                m_DiagonalState.Set(i_PlayerIndex);
            }

            if (IsInAntiDiagonal(i_Pos))
            {
                m_AntiDiagonalState.Set(i_PlayerIndex);
            }
        }

        // Are all states full?
        public bool AreStatesFull()
        {
            return (int)m_TotalNumOfSetFunctionCalls == (r_RowsState.Length * r_ColsState.Length);
        }
        
        // Creates a line state array of same eLineType
        private BoardLineState[] createStateArray(BoardLineState.eLineType i_Type, uint i_AmountOfPlayers, uint i_LineLength, uint i_AmountOfItems)
        {
            BoardLineState[] lineStateArr = new BoardLineState[i_AmountOfItems];
            
            for (int i = 0; i < i_LineLength; i++)
            {
                lineStateArr[i] = new BoardLineState(i_Type, i_AmountOfPlayers, i_LineLength);
            }

            return lineStateArr;
        }
    }
}
