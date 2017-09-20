using System.Collections.Generic;
using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game.State;

namespace C17_Ex05.Game
{
    class GameLogic
    {
        private const int k_FirstAndOnlyItem = 0;
        private readonly Board<GameBoardCell> r_Board;
        private readonly GamePlayers r_Players;
        private readonly GameState r_State;
        private List<uint> m_IndicesOfPlayersThatAreStillPlaying;
        private GameResult? m_GameResult;

        public GameState State
        {
            get
            {
                return r_State;
            }
        }

        // should call IsGameOver atleast once before using this, otherwise an exception will be thrown
        // (This is what we want to happen in that case)
        public GameResult Result
        {
            get
            {
                return (GameResult)m_GameResult;
            }
        }
        
        public List<uint> StillPlayingPlayers
        {
            get
            {
                return m_IndicesOfPlayersThatAreStillPlaying;
            }
        }

        public GameLogic(Board<GameBoardCell> i_Board, GamePlayers i_Players)
        {
            m_GameResult = null;
            r_Board = i_Board;
            r_Players = i_Players;
            r_State = new GameState((uint)i_Players.Length, i_Board.Rows, i_Board.Cols);
            m_IndicesOfPlayersThatAreStillPlaying = new List<uint>((int)r_Players.Length);
            for (uint i = 0; i < r_Players.Length; i++)
            {
                m_IndicesOfPlayersThatAreStillPlaying.Add(i);
            }
        }

        // Set a game-move
        public void Set(Point i_Pos, uint i_PlayerIndex)
        {
            r_Board.Set(i_Pos, r_Players.Get(i_PlayerIndex).GenereateCell());
            r_State.Set(i_Pos, i_PlayerIndex);
            updateIsGameOver(i_Pos);
        }

        // Check if a board cell is empty
        public bool IsEmptyCell(Point i_Move)
        {
            return r_Board.Get(i_Move).Type == GameBoardCell.eType.None;
        }

        // Check if a move is valid
        public bool IsMoveValid(Point i_Move)
        {
            return r_Board.IsInBounds(i_Move) && IsEmptyCell(i_Move);
        }

        // Check is game is over (The actual check is made once after each move)
        public bool IsGameOver()
        {
            return m_GameResult.HasValue;
        }

        public void HandlePlayerQuit(uint i_PlayerIndex)
        {
            m_IndicesOfPlayersThatAreStillPlaying.Remove(i_PlayerIndex);
            m_GameResult = getGameResultIfOnlyOnePlayerOrLessLeft();
        }

        // Check and update if a game is over
        private void updateIsGameOver(Point i_Pos)
        {
            m_GameResult = getGameResultIfGameOver(i_Pos);
        }

        // If game is over returns valid GameResult, otherwise null
        private GameResult? getGameResultIfGameOver(Point i_Pos)
        {
            GameResult? retGameResult = null;
            uint? loserIndex = null;

            retGameResult = getGameResultIfOnlyOnePlayerOrLessLeft();
            if (!retGameResult.HasValue)
            {
                loserIndex = r_State.GetPlayerIfPointInFullLine(i_Pos, m_IndicesOfPlayersThatAreStillPlaying);
                if (loserIndex.HasValue)
                {
                    m_IndicesOfPlayersThatAreStillPlaying.Remove(loserIndex.Value);
                }
                else if (IsBoardFull())
                {
                    m_IndicesOfPlayersThatAreStillPlaying.Clear();
                }

                retGameResult = getGameResultIfOnlyOnePlayerOrLessLeft();
            }

            return retGameResult;
        }

        // If only 1 or 0 players are still in game - returns game result.
        // If its a draw - no players will be left in game
        // If its a win - Only the winner stays in the game.
        private GameResult? getGameResultIfOnlyOnePlayerOrLessLeft()
        {
            GameResult? gameResult = null;

            switch (m_IndicesOfPlayersThatAreStillPlaying.Count)
            {
                case 0:
                    gameResult = new GameResult(GameResult.eResult.Draw);
                    break;
                case 1:
                    gameResult = new GameResult(GameResult.eResult.PlayerWon, m_IndicesOfPlayersThatAreStillPlaying[k_FirstAndOnlyItem]);
                    break;
                default:
                    break;
            }

            return gameResult;
        }

        // Converts Player's index to cell type
        public GameBoardCell.eType PlayerIndexToCellType(uint i_PlayerIndex)
        {
            return r_Players.PlayerIndexToCellType(i_PlayerIndex);
        }

        // Converts GameBoardCell to Player's index in the Player's array.
        public uint CellTypeToPlayerIndex(GameBoardCell.eType i_Type)
        {
            return r_Players.CellTypeToPlayerIndex(i_Type);
        }

        // Is the board Full.
        // note: This cannot be implemented in the Board class itself because there might be games where "Set" is not used uniquely on Board.
        // So the next calculation wont tell us that the board is full.
        public bool IsBoardFull()
        {
            return r_State.AreStatesFull();
        }
    }
}
