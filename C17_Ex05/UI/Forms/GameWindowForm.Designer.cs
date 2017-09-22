using System.Windows.Forms;

namespace C17_Ex05.UI.Forms
{
    partial class GameWindowForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1Score.Location = new System.Drawing.Point(49, 239);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(68, 13);
            this.labelPlayer1Score.TabIndex = 0;
            this.labelPlayer1Score.Text = "Player 1: 0";
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.Location = new System.Drawing.Point(168, 239);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.Size = new System.Drawing.Size(64, 13);
            this.labelPlayer2Score.TabIndex = 1;
            this.labelPlayer2Score.Text = "Computer: 0";
            // 
            // GameWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(270, 270);
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer1Score);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "GameWindowForm";
            this.Text = "TicTacToeMisere";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1Score;
        private System.Windows.Forms.Label labelPlayer2Score;
        private const int k_GameButtonsWidthAndHeight = 20;
        private const int k_SpaceBetweenButtons = 5;
        private const int k_BoardMargin = 7;


        private void generateTableRow(int i_TopOfRow)
        {
            for (int currentColumnIndex = 0; currentColumnIndex < r_AmountOfCols; ++currentColumnIndex)
            {
                Button newButton = new Button
                {
                    Top = i_TopOfRow,
                    Width = k_GameButtonsWidthAndHeight,
                    Height = k_GameButtonsWidthAndHeight
                };
                newButton.Left = k_BoardMargin + (currentColumnIndex * (newButton.Width + k_SpaceBetweenButtons));
                newButton.Enabled = true;
                newButton.Click += boardCellButton_Click;
                Controls.Add(newButton);
            }
        }

        private void generateTable(int i_TopOfRow)
        {
            int topOfRow = k_BoardMargin;
            for (int currentRowIndex = 0; currentRowIndex < r_AmountOfRows; ++currentRowIndex)
            {
                generateTableRow(topOfRow);
                topOfRow += k_GameButtonsWidthAndHeight + k_SpaceBetweenButtons;
            }
        }
    }
}
}