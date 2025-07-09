using System;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class Settings : Form
    {
        private int level = 1;
        public Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;
            
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)//установка уровня
        {
            level = trackBar1.Value;
        }

        public int Level{get{return level; } }

        private void button1_Click(object sender, EventArgs e)//закрыть
        {
            this.Close();
        }
    }
}
