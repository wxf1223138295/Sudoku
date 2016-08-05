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
    public partial class Choose : Form
    {
        public byte valueTemp;        
        public Choose()
        {
            InitializeComponent();
        }
        private void Choose_Load(object sender, EventArgs e)
        {
            this.Height = 90;
            this.Width = 90;
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                {
                    Label gridDeputyform = new Label();
                    gridDeputyform.AutoSize = false;
                    gridDeputyform.Width = 30;
                    gridDeputyform.Height = 30;
                    gridDeputyform.Left = j * 30;
                    gridDeputyform.Top = i * 30;
                    gridDeputyform.ForeColor = Color.Red;
                    gridDeputyform.Font = new Font("", 20, FontStyle.Bold);
                    gridDeputyform.BorderStyle = BorderStyle.Fixed3D;
                    if (i == 0 && j < 3)
                    {
                        if (SUDOKU.xValue[MainWindows.x, j] == j + 1 || SUDOKU.yValue[j, MainWindows.y] == j + 1 || SUDOKU.zValue[(MainWindows.x / 3) * 3 + MainWindows.y / 3, j] == j + 1)
                        {
                            gridDeputyform.Text = string.Format("{0}", (1 + j));
                            gridDeputyform.Name = (1 + j).ToString();
                            gridDeputyform.Enabled = false;
                        }
                        else
                        {
                            gridDeputyform.Text = string.Format("{0}", (1 + j));
                            gridDeputyform.Name = (1 + j).ToString();
                        }
                    }
                    if (i == 1 && j < 3)
                    {
                        if (SUDOKU.xValue[MainWindows.x, j + 3] == j + 4 || SUDOKU.yValue[j + 3, MainWindows.y] == j + 4 || SUDOKU.zValue[(MainWindows.x / 3) * 3 + MainWindows.y / 3, j + 3] == j + 4)
                        {

                            gridDeputyform.Text = string.Format("{0}", (4 + j));
                            gridDeputyform.Name = (4 + j).ToString();
                            gridDeputyform.Enabled = false;
                        }
                        else
                        {
                            gridDeputyform.Text = string.Format("{0}", (4 + j));
                            gridDeputyform.Name = (4 + j).ToString();
                        }
                    }
                    if (i == 2 && j < 3)
                    {
                        if (SUDOKU.xValue[MainWindows.x, j + 6] == j + 7 || SUDOKU.yValue[j + 6, MainWindows.y] == j + 7 || SUDOKU.zValue[(MainWindows.x / 3) * 3 + MainWindows.y / 3, j + 6] == j + 7)
                        {
                            gridDeputyform.Text = string.Format("{0}", (7 + j));
                            gridDeputyform.Name = (7 + j).ToString();
                            gridDeputyform.Enabled = false;
                        }
                        else
                        {
                            gridDeputyform.Text = string.Format("{0}", (7 + j));
                            gridDeputyform.Name = (7 + j).ToString();
                        }
                    }
                    gridDeputyform.TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(gridDeputyform);
                    gridDeputyform.MouseClick += gridDeputyform_MouseClick;
                }
        }
        void gridDeputyform_MouseClick(object sender, MouseEventArgs e)
        {
            Label gridDeputyform = (Label)sender;
            if (e.Button == MouseButtons.Left)
            {
                valueTemp = Convert.ToByte(gridDeputyform.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }
    }
}
