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
        public Data record = new Data("records");
        public Data last_game = new Data("last_game");

        public StartForm()
        {
            InitializeComponent();
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;
            readRecords();



        }
       
        private void readRecords()
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

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "START";
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Text = ":)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this);
            form.Location = this.Location;
            form.Show();
            this.Hide();
            
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
