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
        private readonly uint r_BoardSize;

        //todo: name..
        //todo: choice should start from 1 and not from 0
        public event BoardCellChosenEventHandler BoardCellChosen;

        public GameWindowForm(uint i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            //todo: Create labels according to amount of players
            InitializeComponent();
        }

        public void SetPlayers(string[] i_PlayerNames)
        {

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
            return true; //todo:
        }

        public void ShowMsg(string i_Title, string i_Msg)
        {
            //todo:...
        }
    }
}
