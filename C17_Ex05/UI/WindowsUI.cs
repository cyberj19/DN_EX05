using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game;
using C17_Ex05.UI.Forms;

namespace C17_Ex05.UI
{
    internal class WindowsUI
    {
        private const uint k_BoardMinChoice = 1; //todo: might need to delete this
        private readonly PositiveRange r_BoardRange;
        private readonly ParamsDialogForm r_ParamsDialog; //todo: can be readonly?
        private readonly GameWindowForm r_GameWindow;
        private readonly ReversedTicTacToeParams r_ReversedTicTacToeParams;
        private readonly TwoDimensionalPositiveRange r_BoardCellChoosingRange; // can be changed outside c'tor

        public ReversedTicTacToeParams InitialParams
        {
            get
            {
                return r_ReversedTicTacToeParams;
            }
        }
            

        public WindowsUI(PositiveRange i_BoardRange)
        {
            r_BoardRange = i_BoardRange; //todo: is still required?
            r_ParamsDialog = new ParamsDialogForm(i_BoardRange);
            r_ParamsDialog.Show();
            r_ReversedTicTacToeParams = new ReversedTicTacToeParams(r_ParamsDialog.GameType, r_ParamsDialog.BoardSize);
            r_BoardCellChoosingRange = new TwoDimensionalPositiveRange(k_BoardMinChoice, r_ReversedTicTacToeParams.BoardSize, k_BoardMinChoice, r_ReversedTicTacToeParams.BoardSize);
            r_GameWindow = new GameWindowForm(r_ReversedTicTacToeParams.BoardSize);
        }

        public void Run()
        {
            r_GameWindow.Show();
        }

        public void Clear()
        {
            r_GameWindow.ResetBoard();
        }

        public void RegisterOnInput(BoardCellChosenEventHandler i_EventHandlerFunc) //todo: make sure its ok according to document to do it
        {
            r_GameWindow.BoardCellChosen += i_EventHandlerFunc;
        }

        public void ShowError()
        {
            //todo:??
//            return r_GameWindow.ShowMsg("Error");
        }

        public bool PromptQuestion(string i_Msg)
        {
            return r_GameWindow.PromptQuestion(i_Msg);
        }

        public bool ShouldRunAnotherGame(string i_EndingRoundMsg)
        {

            return true;
//            return PromptQuestion("Would you)
        }

        public void Close()
        {
            //todo:
        }

         /*


        public void PrintCurrentUsersTurn(GameBoardCell.eType i_CurrPlayerCell)
        {
            Console.WriteLine("Its {0} Player's Turn", i_CurrPlayerCell.ToString());
        }

        public void PrintError()
        {
            Console.WriteLine("Unknown error");
        }

        // Prints a goodbye msg
        public void PrintGoodbyeMsg()
        {
            Console.WriteLine("Goodbye!");
        }

        // prints the game results
        public void PrintGameResults(GamePlayers i_GamePlayers, GameResult i_LastGameResult)
        {
            StringBuilder statusStrBuilder = new StringBuilder();

            statusStrBuilder.AppendLine("__________________");
            statusStrBuilder.AppendLine("Game Result:");
            if (i_LastGameResult.Result == GameResult.eResult.Draw)
            {
                statusStrBuilder.AppendLine("Draw");
            }
            else
            {
                string winnerStr = string.Format("{0} Player is the winner", i_GamePlayers.Get(i_LastGameResult.WinPlayerIndex).CellType.ToString());

                statusStrBuilder.AppendLine(winnerStr);
            }

            Console.Write(statusStrBuilder.ToString());
        }

        // asks the user whether we should run another game
        public bool ShouldRunAnotherGame()
        {
            return ConsoleUtils.PromptQuestion("Would you like to play another game?");
        }

        // prints Players status
        public void PrintPlayersStats(GamePlayers i_GamePlayers)
        {
            StringBuilder statusStrBuilder = new StringBuilder();

            statusStrBuilder.AppendLine("Current Status:");

            for (uint i = 0; i < i_GamePlayers.Length; i++)
            {
                GamePlayer currPlayer = i_GamePlayers.Get(i);
                string currPlayerStr = string.Format("{0} Player: {1} Times won", currPlayer.CellType.ToString(), currPlayer.Score);

                statusStrBuilder.AppendLine(currPlayerStr);
            }

            Console.Write(statusStrBuilder.ToString());
        }
        
        // Get User move input
        public Point? GetUserMoveInput(bool isFirstTime)
        {
            Point? retPoint = null;
            string msgStr;

            if (isFirstTime)
            {
                msgStr = "Please insert a move point:";
            }
            else
            {
                msgStr = "Invalid move, Please choose a move point again:";
            }

            Console.WriteLine(msgStr);
            retPoint = ConsoleUtils.GetPointFromUser(m_BoardCellChoosingRange, r_ExitingString, k_BoardMinChoice);

            return retPoint;
        }

        // Ask the user for the required game type
        private GameType getGameType()
        {
            StringBuilder msgBuilder = new StringBuilder();

            msgBuilder.Append("Please insert the required game type:");
            for (uint i = GameType.MinVal; i <= GameType.MaxVal; i++)
            {
                string currLineStr = string.Format("{0}({1}): {2}", Environment.NewLine, i, ((GameType.eGameType)i).ToString());

                msgBuilder.Append(currLineStr);
            }

            PositiveRange gameTypeRange = new PositiveRange(GameType.MinVal, GameType.MaxVal);

            return new GameType(ConsoleUtils.GetPositiveNumberFromUser(msgBuilder.ToString(), gameTypeRange));
        }

        // Ask the user for the required board size
        private uint getBoardSize()
        {
            string requestStr = string.Format("Please insert Board size: ({0}-{1})", r_BoardRange.Min, r_BoardRange.Max);

            return ConsoleUtils.GetPositiveNumberFromUser(requestStr, r_BoardRange);
        }

        public void ClearScreen()
        {
            //Ex02.ConsoleUtils.Screen.Clear();
        }

        public void DrawBoard(Board<GameBoardCell> i_Board)
        {
            ClearScreen();
            ConsoleUtils.DrawBoard(i_Board);
        }
        */

    }
}
