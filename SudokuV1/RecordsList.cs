using System;
using System.Linq;
using System.Windows.Forms;

namespace SudokuV1
{
    public partial class RecordsList : Form
    {
        StartForm form = null;
        public RecordsList(StartForm form)
        {
            this.form = form;
            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            label1.Left = (this.ClientRectangle.Width - label1.Width) / 2;
            button1.Left = (this.ClientRectangle.Width - button1.Width) / 2;

            for (int i = 1; i <= 5; i++)
            {
                Label lbl = this.Controls.Find($"label{i+6}", true).FirstOrDefault() as Label;
                if (lbl != null)
                {
                    lbl.Text = $"Новый текст {i}";
                    if (Time.recordList[i - 1] != null)
                        lbl.Text = Time.recordList[i - 1].ToStr();
                    else
                        lbl.Text = "__:__";
                }
            }
        }

        private void RecordsList_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
