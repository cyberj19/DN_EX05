using System.Collections.Generic;
using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Game
{
    // Manages a Single-Game
    class GameManager
    {
        public enum eMoveResult
        {
            Success,
            GameOver,
            BadInput,
            UnknownFailure
        }

        private const int k_NotFound = -1;
        private readonly GamePlayers r_Players;
        private readonly Board<GameBoardCell> r_Board;
        private readonly GameLogic r_Logic;
        private uint m_CurrPlayersTurn = 0;

        public Board<GameBoardCell> Board
        {
            get
            {
                return r_Board;
            }
        }

        public GameBoardCell.eType CurrentPlayerCellType
        {
            get
            {
                return r_Players.Get(m_CurrPlayersTurn).CellType;
            }
        }

        public GameResult Result
        {
            get
            {
                return r_Logic.Result;
            }
        }

        public GameManager(uint i_BoardSize, GamePlayers i_Players)
        {
            r_Players = i_Players;
            r_Board = new Board<GameBoardCell>(i_BoardSize, i_BoardSize);
            r_Logic = new GameLogic(r_Board, r_Players);
        }

        public void RegisterOnCellChanges(BoardCellSetEventHandler i_HandleBoardCellSetFunc)
        {
            r_Board.BoardCellSet += i_HandleBoardCellSetFunc;
        }


        // Is input required for the next playing player
        public bool IsInputRequiredForCurrentTurn()
        {
            return r_Players.Get(m_CurrPlayersTurn).IsInputRequiredForMove();
        }

        public void HandleCurrUserQuits()
        {
            r_Logic.HandlePlayerQuit(m_CurrPlayersTurn);
        }

        // Is the game over
        public bool IsGameOver()
        {
            return r_Logic.IsGameOver();
        }

        // handle a make move request from the game runner
        public eMoveResult MakeGameMove(Point? i_InputForMove)
        {
            eMoveResult retResult;

            if (IsGameOver())
            {
                retResult = eMoveResult.GameOver;
            }
            else 
            {
                if (IsInputRequiredForCurrentTurn() && (!i_InputForMove.HasValue))
                {
                    retResult = eMoveResult.BadInput;
                }
                else
                {
                    retResult = handleValidMakeMoveRequest(i_InputForMove);
                }
            }

            return retResult;
        }

        // Handle a valid move request
        private eMoveResult handleValidMakeMoveRequest(Point? i_InputForMove)
        {
            eMoveResult retResult;

            Point? currMove = r_Players.Get(m_CurrPlayersTurn).MakeMove(r_Board, r_Logic, i_InputForMove);
            if (!currMove.HasValue)
            {
                retResult = eMoveResult.BadInput;
            }
            else
            {
                if (!r_Logic.IsMoveValid((Point)currMove))
                {
                    retResult = eMoveResult.UnknownFailure;
                }
                else
                {
                    retResult = performMove((Point)currMove);
                }
            }

            return retResult;
        }

        // Perform a move by one of the players
        private eMoveResult performMove(Point i_Move)
        {
            eMoveResult retResult;

            r_Logic.Set(i_Move, m_CurrPlayersTurn);
            if (r_Logic.IsGameOver())
            {
                retResult = eMoveResult.GameOver;
            }
            else
            {
                retResult = eMoveResult.Success;
                m_CurrPlayersTurn = getNewCurrPlayersTurn();
            }

            return retResult;
        }

        // Get the next player that needs to play (Must be in the indices list of currently playing players!)
        private uint getNewCurrPlayersTurn()
        {
            uint nextPlayerTurn = m_CurrPlayersTurn;
            List<uint> currPlaying = r_Logic.StillPlayingPlayers;

            if (currPlaying.Count == 0)
            {
                nextPlayerTurn = 0;
            }
            else
            {
                do
                {
                    nextPlayerTurn++;
                    if (nextPlayerTurn >= r_Players.Length)
                    {
                        nextPlayerTurn = 0;
                    }
                }
                while (k_NotFound == currPlaying.IndexOf(nextPlayerTurn));
            }

            return nextPlayerTurn;
        }
    }
}
