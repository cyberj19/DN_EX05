using C17_Ex05.Game.Player;

namespace C17_Ex05.Game
{
    class GamePlayers
    {
        private const string k_ComputerName = "Computer";
        private const uint k_FirstPlayer = 0;
        private const uint k_SecondPlayer = 1;
        private readonly GamePlayer[] r_Players;

        public uint Length
        {
            get
            {
                return (uint)r_Players.Length;
            }
        }

        // Create Game players instance from GameType
        public static GamePlayers CreateFromGameType(GameType i_GameType, string[] i_PlayerNames)
        {
            GamePlayer[] gamePlayerArr = null;

            switch (i_GameType.Type)
            {
                case GameType.eGameType.PlayerVsComputer:
                    gamePlayerArr = new GamePlayer[]
                    {
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.X, i_PlayerNames[k_FirstPlayer]),
                        new GamePlayer(GamePlayer.eType.ComputerPlayer, GameBoardCell.eType.O, k_ComputerName)
                    };
                    break;
                case GameType.eGameType.PlayerVsPlayer:
                    gamePlayerArr = new GamePlayer[]
                    {
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.X, i_PlayerNames[k_FirstPlayer]),
                        new GamePlayer(GamePlayer.eType.HumanPlayer, GameBoardCell.eType.O, i_PlayerNames[k_SecondPlayer])
                    };
                    break;
            }

            return new GamePlayers(gamePlayerArr);
        }

        public string[] GetNames()
        {
            string[] playerNames = new string[r_Players.Length];

            for (int i = 0; i < r_Players.Length; i++)
            {
                playerNames[i] = r_Players[i].ToString();
            }

            return playerNames;
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
