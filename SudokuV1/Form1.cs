﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Timers;





namespace SudokuV1
{

    public partial class Form1 : Form
    {
       
        public Sudoku s = null;
        private StartForm startForm = null;
        int sec = 0;
        int min = 0;
        System.Timers.Timer timer;
        bool closing = false;
    
        


        private Font newFont = null;
        public Form1(StartForm start)
        {
         
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            startForm = start;
        }

        public Form1(StartForm start, Sudoku s, int min, int sec)
        {

            InitializeComponent();
            startForm = start;
            this.s = s;
            this.sec = sec;
            this.min = min;
        }

        private void Form1_Load(object sender, EventArgs e)//запускает судоку при отерытии формы
        {
            typeof(DataGridView).InvokeMember(
        "DoubleBuffered",
        System.Reflection.BindingFlags.NonPublic |
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.SetProperty,
        null,
        dataGridView1,
        new object[] { true });

            dataGridView1.Left = (this.ClientRectangle.Width - dataGridView1.Width) / 2;
            button1.Left = dataGridView1.Left;
            button2.Left = dataGridView1.Left + 62;
            button3.Left = dataGridView1.Left + 62 * 2;
            button4.Left = dataGridView1.Left + 62 * 3;
            button5.Left = dataGridView1.Left + 62 * 4;
            button6.Left = button1.Left;
            button7.Left = button2.Left;
            button8.Left = button3.Left;
            button9.Left = button4.Left;
            button11.Left = button5.Left;
            Button[] args = { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            dataGridView1.RowCount = 9;
            dataGridView1.ColumnCount = 9;
            int c = Math.Min(dataGridView1.ClientSize.Height, dataGridView1.ClientSize.Width) / 9;
            for (int i = 0; i < 9; i++)
            {
                dataGridView1.Rows[i].Height = c;
                dataGridView1.Columns[i].Width = c;
                dataGridView1.Height = c * 9;
                dataGridView1.Width = c * 9;
            }
            this.DoubleBuffered = true;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Blue;
            Font currentFont = dataGridView1.Font;
            newFont = new Font(currentFont.FontFamily, 12, FontStyle.Regular);
            dataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

            label2.Left = (this.ClientRectangle.Width - label2.Width) / 2;
            start();
        }



        private void button_Click(object sender, EventArgs e) //кнопки для установки цифр
        {
            Button button = sender as Button;
            int row = dataGridView1.SelectedCells[0].RowIndex;
            int column = dataGridView1.SelectedCells[0].ColumnIndex;
            if (s.AddDigit(Int32.Parse(button.Text),row,column))
            {
                dataGridView1.Rows[row].Cells[column].Value = button.Text.Trim();
                dataGridView1.Rows[row].Cells[column].Style.Font = newFont;
                if(s.isAll(Int32.Parse(button.Text)))
                    button.Enabled = false;
            }
            win();

        }

        private void button_Del(object sender, EventArgs e) //кнопка удаления
        {
            
            int row = dataGridView1.SelectedCells[0].RowIndex;
            int column = dataGridView1.SelectedCells[0].ColumnIndex;
            if (s.DelDigit( row, column))
            {
                dataGridView1.Rows[row].Cells[column].Value = null;
             
                
            }



        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)//отрисовка
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;


            // Отрисовка фона и содержимого ячейки
            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);

            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            int selectedCol = dataGridView1.SelectedCells[0].ColumnIndex;
            object selectedValue = dataGridView1.Rows[selectedRow].Cells[selectedCol].Value;

            // Создание кистей для разных типов границ
            Pen thickPen = new Pen(Color.Black, 2);       // Толстая граница
            Pen thinPen = new Pen(Color.Black, 1);       // Тонкая граница
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int c = 0; c < dataGridView1.ColumnCount; c++)
                {
                    var cell = dataGridView1.Rows[r].Cells[c];

                    // Сбрасываем стили
                    cell.Style.BackColor = Color.White;
                   

                    // Выделяем строку и столбец
                    if (r == selectedRow || c == selectedCol)
                    {
                        cell.Style.BackColor = Color.LightBlue;
                    }

                    // Выделяем одинаковые значения
                    if (s!=null && s.UserField[r,c]!=-1 && selectedValue != null && cell.Value != null &&
                        cell.Value.ToString() == selectedValue.ToString())
                    {
                        cell.Style.ForeColor = Color.Blue;
                    }

                }
            }
            // Верхняя граница
            if (e.RowIndex % 3 == 0 || (e.RowIndex == selectedRow && (e.RowIndex - 1) % 3 == 2))
            {
                Point start = new Point(e.CellBounds.Left, e.CellBounds.Top);
                Point end = new Point(e.CellBounds.Right, e.CellBounds.Top);
                e.Graphics.DrawLine( thickPen, start, end);
            }
            else
            {
                Point start = new Point(e.CellBounds.Left, e.CellBounds.Top);
                Point end = new Point(e.CellBounds.Right, e.CellBounds.Top);
                e.Graphics.DrawLine(thinPen, start, end);
            }

            // Левая граница
            if (e.ColumnIndex % 3 == 0 || (e.ColumnIndex == selectedCol && (e.ColumnIndex - 1) % 3 == 2))
            {
                Point start = new Point(e.CellBounds.Left, e.CellBounds.Top);
                Point end = new Point(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.DrawLine(thickPen, start, end);
            }
            else
            {
                Point start = new Point(e.CellBounds.Left, e.CellBounds.Top);
                Point end = new Point(e.CellBounds.Left, e.CellBounds.Bottom);
                e.Graphics.DrawLine(thinPen, start, end);
            }

            // Нижняя граница (для последней строки или блока 3x3)
            if (e.RowIndex == 8 || (e.RowIndex + 1) % 3 == 0)
            {
                Point start = new Point(e.CellBounds.Left, e.CellBounds.Bottom - 1);
                Point end = new Point(e.CellBounds.Right, e.CellBounds.Bottom - 1);
                e.Graphics.DrawLine( thickPen, start, end);
            }

            // Правая граница (для последнего столбца или блока 3x3)
            if (e.ColumnIndex == 8 || (e.ColumnIndex + 1) % 3 == 0)
            {
                Point start = new Point(e.CellBounds.Right - 1, e.CellBounds.Top);
                Point end = new Point(e.CellBounds.Right - 1, e.CellBounds.Bottom);
                e.Graphics.DrawLine( thickPen, start, end);
            }
            

            if (s != null && s.UserField[e.RowIndex, e.ColumnIndex] != s.TrueField[e.RowIndex, e.ColumnIndex] && s.UserField[e.RowIndex, e.ColumnIndex]!=0)
            {
                Rectangle rect = e.CellBounds;
                rect.Inflate(-1, -1); // Немного уменьшаем, чтобы не перекрывать существующие границы
                ControlPaint.DrawBorder(e.Graphics, rect,
                    Color.Red, 3, ButtonBorderStyle.Solid,
                    Color.Red, 3, ButtonBorderStyle.Solid,
                    Color.Red, 3, ButtonBorderStyle.Solid,
                    Color.Red, 3, ButtonBorderStyle.Solid);
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
            }
            else
            {   
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
            }

            
            // Освобождаем ресурсы
            thickPen.Dispose();
            thinPen.Dispose();

            e.Handled = true; // Отключаем стандартную отрисовку границ
        }


        private void drawSudoku() //создает поеле и отрисовывает его
        {
          
            
           

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                 
                    if (s.UserField[i, j] != 0)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = s.UserField[i, j];
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "";
                    }
                }
                
                
            }
            for (int i = 1; i < 10; i++)
            {
                if (s.isAll(i))
                {
                    Button foundButton = this.Controls.Find($"button{i}", true).FirstOrDefault() as Button;
                    foundButton.Enabled = false;
                }
            }
            label1.Text = "" + s.Hints;



        }

        

        public void start() //старт судоку, открытие формы settings и старт таймера
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
            StartForm.last_game.DeleteStr();
            if (s == null)
            {
                sec = 0;
                min = 0;
                label2.Text = "00:00";
                Settings set = new Settings();
                set.Location = new Point(this.Left + (this.Width - set.Width) / 2, this.Top + (this.Width - set.Width) / 2);
                set.ShowDialog();
                s = new Sudoku(set.Level);
            }
            drawSudoku();

            timer = new System.Timers.Timer(1000); // 1000 мс = 1 секунда
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) //работа таймера
        {
            // Обновление UI через Invoke
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    sec++;
                    if (sec == 60)
                    {
                        min++;
                        sec = 0;
                    }
                    label2.Text = $"{min:00}:{sec:00}";
                });
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //закрытие программы
        {
            timer.Stop();
            StartForm.last_game.DeleteStr();
            if (s != null)
            {
                StartForm.last_game.WriteString(s.Level + "");
                StartForm.last_game.WriteString(s.Hints + "");
                StartForm.last_game.WriteSudoku(s.TrueField);
                StartForm.last_game.WriteSudoku(s.Field);
                StartForm.last_game.WriteSudoku(s.UserField);
                StartForm.last_game.WriteString((min * 60 + sec) + "");
            }


            if (!closing)
            {
                Application.Exit();
            }
        }

        private void button12_Click(object sender, EventArgs e) // кнопка подсказки
        {
            if (!s.Solved)
            {
                try
                {
                    int[] pos = s.UseHint();
                
                dataGridView1.Rows[pos[0]].Cells[pos[1]].Value = s.UserField[pos[0], pos[1]];
                dataGridView1.Rows[pos[0]].Cells[pos[1]].Style.Font = newFont;
                label1.Text = "" + s.Hints;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            win();
        }

        private void win() // проверка на победу
        {
            if (s.Solved)
            {
                bool record = false;
                Time time = new Time(sec, min);
                if (time.Record(s.Level)) // записываем время, если оно рекордное для этого уровня сложности
                {
                    record = true;
                    StartForm.record.DeleteStr();
                    foreach (Time tmp in Time.recordList) // переписываем файл, если установлен новый рекорд
                    {
                        if (tmp == null)
                            StartForm.record.WriteString("");
                        else
                            StartForm.record.WriteString(tmp.ToSec() + "");
                    }
                }
                timer.Stop();
                StartForm.last_game.DeleteStr();
                WinScreen ws = new WinScreen(this, record);
                ws.Location = new Point(this.Left + (this.Width - ws.Width) / 2, this.Top + (this.Height - ws.Height) / 2);
                s = null;
                ws.ShowDialog();
                
            }
        }

        private void button10_Click(object sender, EventArgs e) //кнопка паузы
        {
            timer.Stop();
            Pause pause = new Pause(this);
            pause.Location = new Point(this.Left + (this.Width - pause.Width) / 2, this.Top + (this.Height - pause.Height) / 2);
            pause.ShowDialog();
            timer.Start();
         
        }

        public void toMenu() //возврат в главное меню
        {
            startForm.Show();
            closing = true;
            this.Close();
            startForm.toStartScreen();
        }

        public Time getTime() //возвращает время
        {
            return new Time(sec,min);
        }

    
    }
}
