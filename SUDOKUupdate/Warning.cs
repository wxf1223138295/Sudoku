using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGame
{
    public partial class Warning : Form
    {
        public Warning()
        {
            InitializeComponent();
        }
        private void Warning_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.Height = 140;
            this.Width = 240;
            this.Text = "WARNING!";            

            Button buttonReset = new Button();
            buttonReset.Top = 55;
            buttonReset.Left = 60;
            buttonReset.Text = "Reset";
            this.Controls.Add(buttonReset);
            buttonReset.MouseClick += buttonReset_MouseClick;

            Label gridWarning = new Label();
            gridWarning.Top = 18;
            gridWarning.Left = 40;
            gridWarning.Width = 200;
            gridWarning.Height = 150;
            gridWarning.Text = "No Solution!";
            gridWarning.ForeColor = Color.Red;
            gridWarning.Font = new Font("", 15, FontStyle.Bold);
            this.Controls.Add(gridWarning);
        }
        void buttonReset_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;                    
            this.Close();
        }
    }
}
