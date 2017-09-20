using C17_Ex05.Game;
using C17_Ex05.UI;
using C17_Ex05.BasicDataTypes;

namespace C17_Ex05
{
    class ReversedTicTacToe
    {
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

        private const string k_ExitingString = "Q";
        private uint k_MinBoardSize = 3;
        private uint k_MaxBoardSize = 9;
        private ReversedTicTacToeParams m_InitialParams;
        private ConsoleUI m_UI;
        private GamePlayers m_GamePlayers;

        public ReversedTicTacToe()
        {
            m_UI = new ConsoleUI(new PositiveRange(k_MinBoardSize, k_MaxBoardSize), k_ExitingString);
            m_InitialParams = m_UI.GetInitialParams();
            m_GamePlayers = GamePlayers.CreateFromGameType(m_InitialParams.GameType);
        }

        // Main run function, runs as many games as required
        public void run()
        {
            while (runSingleGame() == eSingleGameFuncResult.RunAnotherGame)
            {
                // do nothing, run it again
            }

            m_UI.PrintGoodbyeMsg();
        }

        // run a single move
        private eSingleMoveResult makeSingleMove(GameManager i_GameManager)
        {
            Point? inputForCurrTurn = null;
            GameManager.eMoveResult currMoveResult = GameManager.eMoveResult.Success;
            bool isFirstTimeRequestingInput = true;

            if (i_GameManager.IsInputRequiredForCurrentTurn())
            {
                m_UI.PrintCurrentUsersTurn(i_GameManager.CurrentPlayerCellType);
            }

            do
            {
                if (i_GameManager.IsInputRequiredForCurrentTurn())
                {
                    inputForCurrTurn = m_UI.GetUserMoveInput(isFirstTimeRequestingInput);
                    if (inputForCurrTurn == null)
                    {
                        i_GameManager.HandleCurrUserQuits();
                    }
                }

                currMoveResult = i_GameManager.MakeGameMove(inputForCurrTurn);
                isFirstTimeRequestingInput = false;
            }
            while (currMoveResult == GameManager.eMoveResult.BadInput);

            return handleFinishSingleMove(currMoveResult, i_GameManager);
        }

        private eSingleMoveResult handleFinishSingleMove(GameManager.eMoveResult i_GameMoveResult, GameManager i_GameManager)
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

            m_UI.DrawBoard(i_GameManager.Board);

            return retResult;
        }

        // run a single game
        private eSingleGameFuncResult runSingleGame()
        {
            GameManager manager = new GameManager(m_InitialParams.BoardSize, m_GamePlayers);
            eSingleMoveResult currMoveResult;

            m_GamePlayers.ResetLogicState();
            m_UI.DrawBoard(manager.Board);
            do
            {
                currMoveResult = makeSingleMove(manager);
            }
            while ((!manager.IsGameOver()) && (currMoveResult == eSingleMoveResult.Ok));

            return handleSingleGameFinished(currMoveResult, manager);
        }

        // handle a single game that was finished, according to its last game move result
        private eSingleGameFuncResult handleSingleGameFinished(eSingleMoveResult i_LastGameMoveResult, GameManager i_GameManager)
        {
            eSingleGameFuncResult retSingleGameResult = eSingleGameFuncResult.Finished;

            switch (i_LastGameMoveResult)
            {
                case eSingleMoveResult.Error:
                    m_UI.PrintError();
                    retSingleGameResult = eSingleGameFuncResult.Finished;
                    break;
                case eSingleMoveResult.Ok:
                case eSingleMoveResult.GameOver:
                    retSingleGameResult = handleGameOver(i_GameManager);
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

            m_UI.PrintGameResults(m_GamePlayers, i_GameManager.Result);
            m_UI.PrintPlayersStats(m_GamePlayers);
            if (m_UI.ShouldRunAnotherGame())
            {
                retResult = eSingleGameFuncResult.RunAnotherGame;
            }

            return retResult;
        }
    }
}
