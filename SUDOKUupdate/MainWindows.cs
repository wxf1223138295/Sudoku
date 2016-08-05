using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace SudokuGame
{
    public partial class MainWindows : Form
    {
        public MainWindows()
        {
            InitializeComponent();
            this.BackColor = Color.LightBlue;
        }
        public int times;
        TextBox timeBox = new TextBox();      
        public static int x;
        public static int y;
        public void showLabel()
        {
            for (var i = 0; i < 9; i++)
                for (var j = 0; j < 9; j++)
                {
                    Label gridMainform = new Label();
                    gridMainform.Name = i.ToString() + j.ToString();
                    gridMainform.AutoSize = false;
                    gridMainform.Width = 51;
                    gridMainform.Height = 51;
                    gridMainform.Top = i * 51;
                    gridMainform.Left = j * 51;
                    gridMainform.BorderStyle = BorderStyle.Fixed3D;

                    if (i / 3 == 0 && j / 3 == 0)
                    {
                        gridMainform.BackColor = Color.White;
                    }
                    if (i / 3 == 0 && j / 3 == 1)
                    {
                        gridMainform.BackColor = Color.LightBlue;
                    }
                    if (i / 3 == 0 && j / 3 == 2)
                    {
                        gridMainform.BackColor = Color.White;
                    }
                    if (i / 3 == 1 && j / 3 == 0)
                    {
                        gridMainform.BackColor = Color.LightBlue;
                    }
                    if (i / 3 == 1 && j / 3 == 1)
                    {
                        gridMainform.BackColor = Color.White;
                    }
                    if (i / 3 == 1 && j / 3 == 2)
                    {
                        gridMainform.BackColor = Color.LightBlue;
                    }
                    if (i / 3 == 2 && j / 3 == 0)
                    {
                        gridMainform.BackColor = Color.White;
                    }
                    if (i / 3 == 2 && j / 3 == 1)
                    {
                        gridMainform.BackColor = Color.LightBlue;
                    }
                    if (i / 3 == 2 && j / 3 == 2)
                    {
                        gridMainform.BackColor = Color.White;
                    }

                    gridMainform.TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(gridMainform);
                    gridMainform.MouseClick += gridMainform_MouseClick;
                }
        }
        private void gridMainform_MouseClick(object sender, MouseEventArgs e)
        {
            Choose winChoose = new Choose();
            Label gridMainform = (Label)sender;
            if (e.Button == MouseButtons.Left)
            {
                x = (gridMainform.Top) / 51;
                y = (gridMainform.Left) / 51;
                winChoose.StartPosition = System.Windows.Forms.FormStartPosition.Manual;//手动写位置
                winChoose.Location = new Point(Control.MousePosition.X - 45, Control.MousePosition.Y - 45);
                if (gridMainform.Text == "")
                {
                    var testTemp = System.Windows.Forms.DialogResult.OK;
                    if (winChoose.ShowDialog() == testTemp)
                    {
                        SUDOKU.receive[x, y] = winChoose.valueTemp;
                        SUDOKU.LabelFalse(x, y);

                        gridMainform.Text = Convert.ToString(winChoose.valueTemp);
                        gridMainform.ForeColor = Color.Red;
                        gridMainform.Font = new Font("", 20, FontStyle.Bold);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                x = (gridMainform.Top) / 51;
                y = (gridMainform.Left) / 51;
                if (SUDOKU.receive[x, y] != 0)
                {
                    SUDOKU.LabelCancel(x, y);
                    SUDOKU.receive[x, y] = 0;
                    gridMainform.Text = null;
                    gridMainform.ForeColor = Color.Black;
                }
            }
        }            
        public void SUDOKU_Load(object sender, EventArgs e)
        {


            this.Width = 475;
            this.Height = 560;
            showLabel();
            //开始计算按钮
            Button Calculate = new Button();
            Calculate.Text = "Calculate!";
            Calculate.Width = 100;
            Calculate.Top = 490;
            Calculate.Left = 200;
            this.Controls.Add(Calculate);
            //显示耗时
            Label timeLabel = new Label();
            timeLabel.Top = 492;
            timeLabel.Left = 40;
            timeLabel.Width = 90;
            timeLabel.Text = "Time Consuming:";
            this.Controls.Add(timeLabel);
            //重置按钮
            Button buttonReset = new Button();
            buttonReset.Text = "Reset";
            buttonReset.Width = 100;
            buttonReset.Top = 490;
            buttonReset.Left = 300;
            this.Controls.Add(buttonReset);
            buttonReset.MouseClick += buttonRestart_MouseClick;
            //耗时
            timeBox.Width = 70;
            timeBox.Top = 491;
            timeBox.Left = 130;
            this.Controls.Add(timeBox);
            Calculate.MouseClick += Calculate_MouseClick;
            //数独图片
            //PictureBox pictureSudoku = new PictureBox();
            //pictureSudoku.Left = 70;
            //pictureSudoku.Top = 448;
            //pictureSudoku.Width = 475;
            //pictureSudoku.Height = 80;
            //pictureSudoku.Image = Image.FromFile("C:\\sudoku.png");
            //pictureSudoku.Parent = this;
            //pictureSudoku.SendToBack();
            //this.Controls.Add(pictureSudoku);
        }
        void buttonRestart_MouseClick(object sender, MouseEventArgs e)
        {
            for (int m = 0; m < 9; m++)
            {
                for (int n = 0; n < 9; n++)
                {
                    SUDOKU.receive[m, n] = 0;
                    var tempLable = this.Controls.Find(m.ToString() + n.ToString(), false).First();
                    tempLable.Text = null;
                    tempLable.ForeColor = Color.Black;
                }
            }
            timeBox.Text = null;
            SUDOKU.ArrayClear();
        }
        void Calculate_MouseClick(object sender, MouseEventArgs e)
        {
            int startTime = System.Environment.TickCount;
            if (SUDOKU.RemoveRowColumn() == 0 || SUDOKU.XRemoveGrid() == 0 || SUDOKU.YRemoveGrid() == 0 || SUDOKU.FiveNumble1() == 0 || SUDOKU.FiveNumble2() == 0 || SUDOKU.FiveNumble3() == 0 || SUDOKU.FiveNumble4() == 0 || SUDOKU.FiveNumble5() == 0 || SUDOKU.FiveNumble6() == 0 || SUDOKU.FiveNumble7() == 0 || SUDOKU.FiveNumble8() == 0 || SUDOKU.FiveNumble9() == 0 || SUDOKU.FiveNumble10() == 0 || SUDOKU.FiveNumble11() == 0 || SUDOKU.FiveNumble12() == 0 || SUDOKU.XNineGrid() == 0 || SUDOKU.YNineGrid() == 0 || SUDOKU.RoundRemove() == 0)
            {
                Warning warning = new Warning();
                if (warning.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    for (int m = 0; m < 9; m++)
                    {
                        for (int n = 0; n < 9; n++)
                        {
                            SUDOKU.receive[m, n] = 0;
                            var tempLable = this.Controls.Find(m.ToString() + n.ToString(), false).First();
                            tempLable.Text = null;
                            tempLable.ForeColor = Color.Black;
                        }
                    }
                    timeBox.Text = null;
                    SUDOKU.ArrayClear();
                    SUDOKU.countTimes = 2;
                }
            }
            else if (SUDOKU.LongTime() == 1 && SUDOKU.countTimes == 1)
            {
                for (int k = 0; k < 9; k++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        var tempLable = this.Controls.Find(k.ToString() + j.ToString(), false).First();
                        tempLable.Text = string.Format("{0}", SUDOKU.receive[k, j]);
                        tempLable.Font = new Font("", 20, FontStyle.Bold);
                    }
                }
            }
            else
            {
                SUDOKU.GetResult(0);
                for (int k = 0; k < 9; k++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        var tempLable = this.Controls.Find(k.ToString() + j.ToString(), false).First();
                        tempLable.Text = string.Format("{0}", SUDOKU.Rereceive[k, j]);
                        tempLable.Font = new Font("", 20, FontStyle.Bold);
                    }
                }
            }                     
            int endTime = System.Environment.TickCount;
            int runTime = endTime - startTime;
            if (SUDOKU.countTimes == 1)
            {
                timeBox.Text = string.Format("{0}ms", runTime);
            }
            SUDOKU.countTimes = 0;
            times = 0;
        }
    }
}
