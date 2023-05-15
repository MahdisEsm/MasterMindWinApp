using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMindWinApp
{
    public partial class Form1 : Form
    {
        public Game gameInstance;
        public int x = 5;
        public int y = 50;
        public List<TextBox> txt = new List<TextBox>();
        private int[] _sysArray;
        private int[] _userArray;
        public Form1()
        {
            InitializeComponent();

        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            gameInstance = new Game(5);
            _userArray = new int[gameInstance._dimension];
            _sysArray = gameInstance.GenerateRandomNum();
            startPanel.Visible = false;
            FillTextBox();
        }
        private void mediumButton_Click(object sender, EventArgs e)
        {

            gameInstance = new Game(6);
            _userArray = new int[gameInstance._dimension];
            _sysArray = gameInstance.GenerateRandomNum();
            startPanel.Visible = false;
            FillTextBox();
        }
        private void hardButton_Click(object sender, EventArgs e)
        {
            gameInstance = new Game(7);
            _userArray = new int[gameInstance._dimension];
            _sysArray = gameInstance.GenerateRandomNum();
            startPanel.Visible = false;
            FillTextBox();

        }
        private void veryHardButton_Click(object sender, EventArgs e)
        {
            gameInstance = new Game(8);
            _userArray = new int[gameInstance._dimension];
            _sysArray = gameInstance.GenerateRandomNum();
            startPanel.Visible = false;
            FillTextBox();
        }

        public void FillTextBox()
        {
            txt.Clear();
            for (int i = 0; i < gameInstance._dimension; i++)
            {
                txt.Add(new TextBox() { Size = new System.Drawing.Size(50, 25), MaxLength = 1 });
            }
            txt.ForEach(t => t.TextChanged += new EventHandler(this.T_TextChanged));
            txt.ForEach(t => t.KeyPress += new KeyPressEventHandler(this.T_KeyPress));
            txt[gameInstance._dimension - 1].TextChanged += new EventHandler(this.T_lastTextBox);
            AddTextBox();
        }

        private void T_lastTextBox(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label>();
            for (int i = 0; i < gameInstance._dimension; i++)
            {
                labels.Add(new Label() { Size = new System.Drawing.Size(10, 25) });
            }
            int lableX = (gameInstance._dimension * 55) + 10;
            foreach (var item in labels)
            {
                item.Location = new Point(lableX, y - 30);
                this.Controls.Add(item);
                lableX += 15;
            }

            int[] finalresult = gameInstance.CheckNumbers(_userArray, _sysArray);

            for (int i = 0; i < finalresult.Length; i++)
            {
                if (finalresult[i] == 0)
                {
                    labels[i].ForeColor = Color.Red;
                    labels[i].Text = _userArray[i].ToString();

                }
                else if (finalresult[i] == 1)
                {

                    labels[i].ForeColor = Color.Green;
                    labels[i].Text = _userArray[i].ToString();


                }
                else
                {

                    labels[i].ForeColor = Color.Yellow;
                    labels[i].Text = _userArray[i].ToString();

                }
            }
            if (finalresult.All(a => a == 1))
            {
                DialogResult result= MessageBox.Show("You Win!\nDo you want to play again?"," ",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
                if(result==DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                FillTextBox();
            }
        }

        private void T_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            int r = 0;
            int index = txt.FindIndex(a => a == (sender as TextBox));
            if (int.TryParse((sender as TextBox).Text, out r))
            {
                _userArray[index] = int.Parse((sender as TextBox).Text);
            }


            if (((TextBox)sender).TextLength >= 1)
            {
                SendKeys.Send("{Tab}");
            }

        }

        private void AddTextBox()
        {
            foreach (var item in txt)
            {
                item.Location = new Point(x, y);
                this.Controls.Add(item);
                x += 55;
                System.Threading.Thread.Sleep(10);
            }
            x = 5;
            y += 30;
        }
        

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            
                if(dialogResult==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ToolStripMenuItemNewGame_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to start new game?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if(dialogResult==DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}
