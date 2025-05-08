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
    public partial class Pause : Form
    {
        private Form1 form1 = null;
        public Pause(Form1 form)
        {
            InitializeComponent();
            form1 = form;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;
            button2.Left = (this.ClientRectangle.Width - button2.Width) / 2;
            button3.Left = (this.ClientRectangle.Width - button3.Width) / 2;
            
        }

        private void button1_Click(object sender, EventArgs e) //продолжить игру
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//начать сначала
        {
            this.Close();
            form1.start();

        }

        private void button3_Click(object sender, EventArgs e)//выход в главное меню
        {
            this.Close();
            form1.toMenu();
        }
    }
}
