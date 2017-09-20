using C17_Ex05.Game.Player;

namespace C17_Ex05.Game
{
    class GamePlayers
    {
        private readonly GamePlayer[] r_Players;

        public uint Length
        {
            get
            {
                return (uint)r_Players.Length;
            }
        }

        // Create Game players instance from GameType
        public static GamePlayers CreateFromGameType(GameType i_GameType)
        {
            GamePlayer[] gamePlayerArr = null;

            switch (i_GameType.Type)
            {
                case GameType.eGameType.PlayerVsComputer:
                    gamePlayerArr = new GamePlayer[]
                    {
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.X),
                        new GamePlayer(GamePlayer.eType.ComputerPlayer, GameBoardCell.eType.O)
                    };
                    break;
                case GameType.eGameType.PlayerVsPlayer:
                    gamePlayerArr = new GamePlayer[]
                    {
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.X),
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.O)
                    };
                    break;
            }

            return new GamePlayers(gamePlayerArr);
        }

        private GamePlayers(GamePlayer[] i_Players)
        {
            r_Players = i_Players;
        }

        public GamePlayer Get(uint i_PlayerId)
        {
            return r_Players[i_PlayerId];
        }

        public void AddScore(uint i_PlayerId)
        {
            r_Players[i_PlayerId].AddScore();
        }

        public void ResetLogicState()
        {
            foreach(GamePlayer player in r_Players)
            {
                player.ResetLogicState();
            }
        }

        // Converts Player's index to cell type
        public GameBoardCell.eType PlayerIndexToCellType(uint i_PlayerIndex)
        {
            return r_Players[i_PlayerIndex].CellType;
        }

        // Converts GameBoardCell to Player's index in the Player's array.
        public uint CellTypeToPlayerIndex(GameBoardCell.eType i_Type)
        {
            uint? retIndex = null;

            if (i_Type != GameBoardCell.eType.None)
            {
                for (uint i = 0; i < r_Players.Length; i++)
                {
                    if (i_Type == r_Players[i].CellType)
                    {
                        retIndex = i;
                    }
                }
            }

            return (uint)retIndex;
        }
    }
}
