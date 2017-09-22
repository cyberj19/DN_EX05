using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game;
using C17_Ex05.Game.Player;
using C17_Ex05.UI.Forms;
using System;
using System.Windows.Forms;

namespace C17_Ex05.UI
{
    internal class WindowsUI
    {
        private const uint k_BoardMinChoice = 1; //todo: might need to delete this
        private readonly PositiveRange r_BoardRange;
        //private readonly ParamsDialogForm r_ParamsDialog; //todo: can be readonly?
        private readonly GameWindowForm r_GameWindow;
        private readonly ReversedTicTacToeParams r_ReversedTicTacToeParams;
        private readonly TwoDimensionalPositiveRange r_BoardCellChoosingRange; // can be changed outside c'tor

        internal BoardCellSetEventHandler BoardCellSetHandler
        {
            get
            {
                return r_GameWindow.BoardCellSetHandler;
            }
        }

        public ReversedTicTacToeParams InitialParams
        {
            get
            {
                return r_ReversedTicTacToeParams;
            }
        }
            

        public WindowsUI(string i_Title, PositiveRange i_BoardRange)
        {
            r_BoardRange = i_BoardRange; //todo: is still required?
            // r_ParamsDialog = new ParamsDialogForm(i_BoardRange); //todo: ENFORCE MAX TEXT SIZE!!!!!!!!!!!! otherwise names very long.. also enforce computer name
            // r_ParamsDialog.Show();
            //            r_ReversedTicTacToeParams = new ReversedTicTacToeParams(r_ParamsDialog.GameType, r_ParamsDialog.BoardSize, r_ParamsDialog.PlayerNames);
            r_ReversedTicTacToeParams = new ReversedTicTacToeParams(new GameType(GameType.eGameType.PlayerVsComputer), 5, new string[] { "Player 1", "Computer" });
            r_BoardCellChoosingRange = new TwoDimensionalPositiveRange(k_BoardMinChoice, r_ReversedTicTacToeParams.BoardSize, k_BoardMinChoice, r_ReversedTicTacToeParams.BoardSize);
            r_GameWindow = new GameWindowForm(i_Title, r_ReversedTicTacToeParams.BoardSize);
        }

        public void Run()
        {
            r_GameWindow.ShowDialog();
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
            r_GameWindow.ShowMsg("Error", "Error");
        }

        public bool PromptQuestion(string i_Title, string i_Msg)
        {
            return r_GameWindow.PromptQuestion(i_Title, i_Msg);
        }

        public bool ShouldRunAnotherGame(GamePlayers i_GamePlayers, GameResult i_LastGameResult)
        {
            string resultStr;
            string msgboxTitle;
            string msgboxText;

            if (i_LastGameResult.Result == GameResult.eResult.Draw)
            {
                resultStr = "Tie!";
                msgboxTitle = "A Tie!";
            }
            else
            {
                resultStr = string.Format("The winner is {0}!", i_GamePlayers.Get(i_LastGameResult.WinPlayerIndex).CellType.ToString());
                msgboxTitle = "A Win!";
            }

            msgboxText = string.Format("{0}{1}Would you like to play another round?", resultStr, Environment.NewLine);

            return PromptQuestion(msgboxTitle, msgboxText);
        }

        public void InitWindow(GamePlayers i_GamePlayers)
        {
            r_GameWindow.Init(i_GamePlayers.GetNames());
        }

        public void UpdatePlayersStats(GamePlayers i_GamePlayers)
        {
            for (uint i = 0; i < i_GamePlayers.Length; i++)
            {
                GamePlayer currPlayer = i_GamePlayers.Get(i);

                r_GameWindow.UpdatePlayerStat(i, currPlayer.Score);
            }
        }
        
        public void Close()
        {
            r_GameWindow.Close();
            Application.Exit();
        }
    }
}
