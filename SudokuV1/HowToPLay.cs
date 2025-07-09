using System;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class HowToPLay : Form
    {
        StartForm form;
        public HowToPLay(StartForm form)
        {
            InitializeComponent();
            this.form = form;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HowToPLay_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Show();
        }
    }
}
