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

    //todo: name..
    //todo: choice should start from 1 and not from 0

    public partial class GameWindowForm : Form
    {
        private const int k_ButtonTopStart = 20;
        private const int k_ButtonTopPadding = 5;
        private const int k_ButtonLeftPadding = 10;
        private const int k_ButtonLeftStart = 10;
        private const int k_ButtonHeight = 50;
        private const int k_ButtonWidth = k_ButtonHeight;
        private const int k_LabelHeight = 15;
        private const int k_LabelTopPadding = 10;
        private const int k_LabelLeftPadding = 10;


        public event BoardCellChosenEventHandler BoardCellChosen;
        private readonly int r_BoardSize;
        private readonly Button[,] r_BoardButtons; //todo: Saving them twice.. both in controls and here
        private string[] m_PlayerNames;
        private Label[] m_PlayersLabels;
        private uint m_CurrentPlayersTurn;

        internal BasicDataTypes.BoardCellSetEventHandler BoardCellSetHandler
        {
            get
            {
                return Board_BoardCellSet;
            }
        }

        internal Game.UserTurnChangedEventHandler UserTurnChangedHandler
        {
            get
            {
                return GameManager_UserTurnChanged;
            }
        }

        public GameWindowForm(string i_Title, uint i_BoardSize)
        {
            r_BoardSize = (int)i_BoardSize;
            r_BoardButtons = new Button[r_BoardSize, r_BoardSize];
            InitializeComponent();
            Text = i_Title;
            m_CurrentPlayersTurn = 0;
        }

        public void Init(string[] i_PlayerNames)
        {
            m_PlayerNames = i_PlayerNames;
            initFormProperties();
            createBoardButtons();
            createPlayersLabels();
        }

        private void initFormProperties()
        {
            ClientSize = new Size(generateFormWidth(), generateFormHeight());
            //todo: Decide whether should allow resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private int generateFormWidth()
        {
            return (k_ButtonWidth + k_ButtonLeftPadding) * r_BoardSize + 2 * k_ButtonLeftStart; //todo: add const
        }

        private int generateFormHeight()
        {
            return (k_ButtonHeight + k_ButtonTopPadding) * r_BoardSize + k_ButtonTopStart + 2 * k_LabelTopPadding + k_LabelHeight;
        }

        private void createBoardButtons()
        {
            int currLeft;
            int currTop = k_ButtonTopStart;

            for (int currRow = 0; currRow < r_BoardSize; currRow++)
            {
                currLeft = k_ButtonLeftStart;
                for (int currCol = 0; currCol < r_BoardSize; currCol++)
                {
                    Button currButton = new Button();

                    currButton.Click += BoardButton_Click; //todo: consider making lambda with pos
                    currButton.Left = currLeft;
                    currButton.Font = new Font(currButton.Font.FontFamily, currButton.Font.Size, FontStyle.Bold);
                    currButton.Top = currTop;
                    currButton.Height = k_ButtonHeight;
                    currButton.Width = k_ButtonWidth;
                    currButton.TabStop = false;
                    Controls.Add(currButton);
                    r_BoardButtons[currRow, currCol] = currButton;
                    currLeft += currButton.Width + k_ButtonLeftPadding;
                }

                currTop += k_ButtonHeight + k_ButtonTopPadding;
            }
        }
        

        private void BoardButton_Click(object sender, EventArgs e)
        {
            //todo: name
            OnBoardCellChosen(findButtonPos((Button)sender));
        }

        //todo: name
        private void GameManager_UserTurnChanged(uint i_UserIndex)
        {
            Label currBoldLabel = m_PlayersLabels[m_CurrentPlayersTurn];
            Label newBoldLabel = m_PlayersLabels[i_UserIndex];

            m_CurrentPlayersTurn = i_UserIndex;
            currBoldLabel.Font = new Font(currBoldLabel.Font.Name, currBoldLabel.Font.Size, FontStyle.Regular);
            newBoldLabel.Font = new Font(newBoldLabel.Font.Name, newBoldLabel.Font.Size, FontStyle.Bold);
        }

        private BasicDataTypes.Point findButtonPos(Button i_Button)
        {
            BasicDataTypes.Point? ret = null;

            for (uint currRow = 0; currRow < r_BoardSize; currRow++)
            {
                for (uint currCol = 0; currCol < r_BoardSize; currCol++)
                {
                    if (r_BoardButtons[currRow, currCol].Equals(i_Button))
                    {
                        ret = new BasicDataTypes.Point(currCol, currRow);
                    }
                }
            }

            // if not found, an excpetion will be thrown
            return ret.Value;
        }

        protected virtual void OnBoardCellChosen(BasicDataTypes.Point i_Pos)
        {
            if (BoardCellChosen != null)
            {
                BoardCellChosen.Invoke(i_Pos);
            }
        }

        private void createPlayersLabels()
        {
            int currLeft = ClientSize.Width / 2 - k_ButtonWidth - k_ButtonLeftPadding;
            int labelTop = ClientSize.Height - k_LabelHeight - k_LabelTopPadding;

            m_PlayersLabels = new Label[m_PlayerNames.Length];

            for (int i = 0; i < m_PlayerNames.Length; i++)
            {
                Label currLabel = new Label();

                if (i == 0)
                {
                    currLabel.Font = new Font(currLabel.Font.Name, currLabel.Font.Size, FontStyle.Bold);
                }

                currLabel.Top = labelTop;
                currLabel.Left = currLeft;
                currLabel.Text = string.Format("{0}: 0  ", m_PlayerNames[i]); //todo: max score 100 without overriding other score?...
                currLabel.AutoSize = true;
                Controls.Add(currLabel);
                m_PlayersLabels[i] = currLabel;
                currLeft += currLabel.Width + k_LabelLeftPadding;
            }
        }

        //todo: name..
        private void Board_BoardCellSet(BasicDataTypes.Point i_Pos, string i_ValueStr)
        {
            r_BoardButtons[i_Pos.Y, i_Pos.X].Text = i_ValueStr;
            r_BoardButtons[i_Pos.Y, i_Pos.X].Enabled = false;
        }

        public void UpdatePlayerStat(uint i_PlayerIndex, uint i_Score)
        {

        }

        public void ResetBoard()
        {
            for (uint currRow = 0; currRow < r_BoardSize; currRow++)
            {
                for (uint currCol = 0; currCol < r_BoardSize; currCol++)
                {
                    Button currButton = r_BoardButtons[currRow, currCol];

                    currButton.Text = string.Empty;
                    currButton.Enabled = true;
                }
            }
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
