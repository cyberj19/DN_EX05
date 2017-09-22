using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C17_Ex05.UI.Forms
{
    //todo: verify name
    public delegate bool BoardCellChosenEventHandler(BasicDataTypes.Point i_Pos);

    public partial class GameWindowForm : Form
    {
        public event BoardCellChosenEventHandler BoardCellChosen;
        private readonly uint r_BoardSize;
        private string[] m_PlayerNames;

        //todo: name..
        //todo: choice should start from 1 and not from 0


        private Label m_PlayerLabels;
        public GameWindowForm(uint i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            //todo: Create labels according to amount of players
            InitializeComponent();
            createBoardButtons();
            createPlayersLabels();
        }

        private void createBoardButtons()
        {

        }

        private void createPlayersLabels()
        {

        }

        public void SetPlayers(string[] i_PlayerNames)
        {
            m_PlayerNames = i_PlayerNames;
            //todo : generate labels
        }

        public void UpdatePlayerStat(uint i_PlayerIndex, uint i_Score)
        {

        }

        public void ResetBoard()
        {
            //todo:... 
            //todo: Perhaps create this instance again instead of reseting it.
        }

        public bool PromptQuestion(string i_Title, string i_Msg)
        {
            DialogResult dialogResult = MessageBox.Show(i_Msg, i_Title, MessageBoxButtons.YesNo);

            return dialogResult == DialogResult.Yes;
        }

        public void ShowMsg(string i_Title, string i_Msg)
        {
            MessageBox.Show(i_Msg, i_Title);
        }
    }
}
