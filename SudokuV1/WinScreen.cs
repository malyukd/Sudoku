using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class WinScreen : Form
    {
        private Form1 form = null;
        public WinScreen(Form1 form)
        {
            InitializeComponent();
            Time time = form.getTime();
            label2.Text =time.ToStr();
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            label2.Left = (this.ClientRectangle.Width - label2.Width) / 2;
            this.form = form;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //начать сначала
        {
            this.Close();
            form.start();
        }

        private void button3_Click(object sender, EventArgs e)//вернуться в меню
        {
            this.Close();
            form.toMenu();
        }
    }
}
