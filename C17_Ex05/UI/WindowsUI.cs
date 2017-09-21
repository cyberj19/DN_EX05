using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game;
using C17_Ex05.Game.Player;
using C17_Ex05.UI.Forms;
using System;

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

        public void SetPlayers(GamePlayers i_GamePlayers)
        {
            r_GameWindow.SetPlayers(i_GamePlayers.GetNames());
        }

        public void UpdatePlayersStats(GamePlayers i_GamePlayers)
        {
            for (uint i = 0; i < i_GamePlayers.Length; i++)
            {
                GamePlayer currPlayer = i_GamePlayers.Get(i);
//                string currPlayerStr = string.Format("{0} Player: {1} Times won", currPlayer.CellType.ToString(), );

                r_GameWindow.UpdatePlayerStat(i, currPlayer.Score);
            }

            /*
             StringBuilder statusStrBuilder = new StringBuilder();

            statusStrBuilder.AppendLine("Current Status:");

            for (uint i = 0; i < i_GamePlayers.Length; i++)
            {
                GamePlayer currPlayer = i_GamePlayers.Get(i);
                string currPlayerStr = string.Format("{0} Player: {1} Times won", currPlayer.CellType.ToString(), currPlayer.Score);

                statusStrBuilder.AppendLine(currPlayerStr);
            }

            Console.Write(statusStrBuilder.ToString());
             */

        }

        //todo: delegate instead
        public void UpdateCurrentUsersTurn(GameBoardCell.eType i_CurrPlayerCell)
        {

        }

        public void Close()
        {
            //todo:
        }
    }
}
