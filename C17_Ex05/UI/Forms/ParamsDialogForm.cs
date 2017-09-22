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
    public partial class ParamsDialogForm : Form
    {
        public ParamsDialogForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxPlayer2.Enabled = !this.textBoxPlayer2.Enabled;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown2.Value = this.numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = this.numericUpDown2.Value;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
