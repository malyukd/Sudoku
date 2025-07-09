using System;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class WinScreen : Form
    {
        private Form1 form = null;
        public WinScreen(Form1 form, bool record)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            Time time = form.getTime();
            label2.Text =time.ToStr();
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            label2.Left = (this.ClientRectangle.Width - label2.Width) / 2;
            label3.Left = (this.ClientRectangle.Width - label3.Width) / 2;
            this.form = form;
            if (!record)
                label3.Hide();
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
