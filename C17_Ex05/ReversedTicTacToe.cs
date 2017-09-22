using C17_Ex05.Game;
using C17_Ex05.UI;
using C17_Ex05.BasicDataTypes;

namespace C17_Ex05
{
    //todo: internal on all classes..
    internal class ReversedTicTacToe
    {
        //todo: change this enum name
        private enum eSingleGameFuncResult
        {
            Finished,
            RunAnotherGame
        }

        private enum eSingleMoveResult
        {
            Ok,
            Error,
            GameOver,
        }

        private uint k_MinBoardSize = 3;
        private uint k_MaxBoardSize = 9;
        private WindowsUI m_UI;
        private GamePlayers m_GamePlayers;
        private GameManager m_CurrGameManager;

        public ReversedTicTacToe()
        {
            m_UI = new WindowsUI(new PositiveRange(k_MinBoardSize, k_MaxBoardSize));
            m_GamePlayers = GamePlayers.CreateFromGameType(m_UI.InitialParams.GameType, m_UI.InitialParams.PlayerNames);
            m_UI.RegisterOnInput(this.windowsUI_BoardCellChosen);
            m_UI.InitWindow(m_GamePlayers);
            startNewGame();
        }

        // Main run function, runs as many games as required
        public void Run()
        {
            m_UI.Run();
        }

        //todo: is good name?... its not the actual object, but its a proxy so thats why WindowsUI_....
//        public bool handleInput(Point i_Input)
        private bool windowsUI_BoardCellChosen(Point i_Input)
        {
            eSingleMoveResult currMoveResult;
            Point? currInput = i_Input;
            //todo: throw if not running..

            // first time will run player with input, all next times players without input required (Computer)
            do
            {
                currMoveResult = makeSingleMove(currInput);
                currInput = null;
                if (m_CurrGameManager.IsGameOver() || (currMoveResult != eSingleMoveResult.Ok))
                {
                    if (handleSingleGameFinished(currMoveResult) != eSingleGameFuncResult.RunAnotherGame)
                    {
                        m_UI.Close();
                    }
                }
            }
            while (!m_CurrGameManager.IsInputRequiredForCurrentTurn());

            //todo: do this also for the computer!
            //m_UI.UpdateCurrentUsersTurn(m_CurrGameManager.CurrentPlayerCellType);

            //todo: when to return false and when to write about error?
            return true;
        }

        private void startNewGame()
        {
            m_CurrGameManager = new GameManager(m_UI.InitialParams.BoardSize, m_GamePlayers);
            m_GamePlayers.ResetLogicState();
            m_UI.Clear();
        }

        // run a single move
        private eSingleMoveResult makeSingleMove(Point? i_Input)
        {
            GameManager.eMoveResult currMoveResult = m_CurrGameManager.MakeGameMove(i_Input);

            return handleFinishSingleMove(currMoveResult);
        }

        private eSingleMoveResult handleFinishSingleMove(GameManager.eMoveResult i_GameMoveResult)
        {
            eSingleMoveResult retResult = eSingleMoveResult.Ok;

            switch (i_GameMoveResult)
            {
                case GameManager.eMoveResult.BadInput:
                case GameManager.eMoveResult.UnknownFailure:
                    retResult = eSingleMoveResult.Error;
                    break;
                case GameManager.eMoveResult.GameOver:
                    retResult = eSingleMoveResult.GameOver;
                    break;
                default:
                    retResult = eSingleMoveResult.Ok;
                    break;
            }

            return retResult;
        }
        
        // handle a single game that was finished, according to its last game move result
        private eSingleGameFuncResult handleSingleGameFinished(eSingleMoveResult i_LastGameMoveResult)
        {
            eSingleGameFuncResult retSingleGameResult = eSingleGameFuncResult.Finished;

            switch (i_LastGameMoveResult)
            {
                case eSingleMoveResult.Error:
                    m_UI.ShowError();
                    retSingleGameResult = eSingleGameFuncResult.Finished;
                    break;
                case eSingleMoveResult.Ok:
                case eSingleMoveResult.GameOver:
                    retSingleGameResult = handleGameOver(m_CurrGameManager);
                    break;
            }

            return retSingleGameResult;
        }

        private eSingleGameFuncResult handleGameOver(GameManager i_GameManager)
        {
            eSingleGameFuncResult retResult = eSingleGameFuncResult.Finished;

            GameResult gameResult = i_GameManager.Result;

            if (gameResult.Result == GameResult.eResult.PlayerWon)
            {
                m_GamePlayers.AddScore(gameResult.WinPlayerIndex);
            }

            m_UI.UpdatePlayersStats(m_GamePlayers);
            if (m_UI.ShouldRunAnotherGame(m_GamePlayers, i_GameManager.Result))
            {
                retResult = eSingleGameFuncResult.RunAnotherGame;
            }

            return retResult;
        }
    }
}
