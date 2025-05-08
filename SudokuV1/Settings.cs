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
    public partial class Settings : Form
    {
        private int level = 1;
        public Settings()
        {
            InitializeComponent();
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;
            
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            level = trackBar1.Value;
        }

        public int Level{get{return level; } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
