using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class StartForm : Form
    {
        public static Data record = new Data("records");
        public static Data last_game = new Data("last_game");
        Sudoku s = null;
        int min = 0;
        int sec = 0;

        public StartForm()
        {
            InitializeComponent();
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;
            readRecords();
            toStartScreen();

        }

        public void toStartScreen()
        {
            if (!last_game.Read().Equals(""))
            {
                button3.Enabled = true;
                readLastGame();
            }else
                button3.Enabled=false;
        }
       
        private void readRecords()//читаем файл с рекордами
        {
            string t = record.Read();
            string[] text  = t.Split('\n');
            if (text.Length == 1)
                return;
            for(int i=0; i<5; i++)
            {
                if (text[i]!="")
                    Time.recordList[i] = new Time(Convert.ToInt32(text[i]));
            }
           
        }

        private void readLastGame()
        {
            string t = last_game.Read();
            string[] text = t.Split('\n');
            int level = int.Parse(text[0]);
            int hints = int.Parse(text[1]);
            int time = int.Parse(text[text.Length-2]);
            min = time / 60;
            sec = time % 60;
            int[,] truefield = new int[9, 9];
            int[,] field = new int[9, 9];
            int[,] userfield = new int[9, 9];
            for (int i = 2; i < 11; i++)
            {
                string[] line = text[i].Split(' ');
                for(int j = 0; j<9; j++)
                {
                    truefield[i-2,j]= int.Parse(line[j]);
                }
            }
            for (int i = 11; i < 20; i++)
            {
                string[] line = text[i].Split(' ');
                for (int j = 0; j < 9; j++)
                {
                    field[i - 11, j] = int.Parse(line[j]);
                }
            }
            for (int i = 20; i < 29; i++)
            {
                string[] line = text[i].Split(' ');
                for (int j = 0; j < 9; j++)
                {
                    userfield[i - 20, j] = int.Parse(line[j]);
                }
            }
            s = new Sudoku(truefield, field, userfield, level, hints);
            last_game.DeleteStr();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "START";
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Text = ":)";
        }

        private void button1_Click(object sender, EventArgs e)//старт игры
        {
            Form1 form = new Form1(this);
            form.Location = this.Location;
            form.Show();
            this.Hide();
            last_game.DeleteStr();
            
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)//закрытие формы
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this, s, min, sec);
            form.Location = this.Location;
            form.Show();
            s = null;
            min = 0;
            sec = 0;
            this.Hide();
        }
    }
}
