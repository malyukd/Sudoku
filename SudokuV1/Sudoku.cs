using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuV1
{
    public class Sudoku
    {
        private int[,] trueField; // решенный судоку
        private int[,] field; // изначальное поле
        private int[,] userfield; //поле пользователя
        private bool solved; //решено или нет
        private int level; //уровень сложности
        private Random random; //рандомайзер
        private int hints = 99; //подсказки

        public Sudoku(int level) {  //конструктор
            this.random = new Random();
            this.level = (level + 1) * 10;
            SudokuGen();
        }

        public int[,] TrueField {  get { return this.trueField; } }
        public int[,] Field { get { return this.field; } }
        public int[,] UserField { get {return this.userfield; } }
        public bool Solved { get { return this.solved; } }
        public int Hints {  get { return this.hints; } }
        public int Level {  get { return this.level/10-1; } }


        public bool AddDigit(int digit, int x, int y) { //добавление цифры
            if (this.userfield[x, y] == 0 || this.userfield[x, y] != this.trueField[x, y])
            {
                this.userfield[x, y] = digit;
               
                Check();
                return true;
            }
            else
                return false;
        }
        public bool DelDigit(int x, int y) //удаление цифры
        {
            if (this.userfield[x, y] != this.trueField[x,y])
            {
                this.userfield[x, y] = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isAll(int dig) //проверка на то, поставлены ли все цифры
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.userfield[i, j] == dig)
                        count++;
                }
            }
            if (count == 9)
                return true;
            else
                return false;
        }
        public int[]  UseHint() //подсказка
        {
            if (this.hints == 0)
                throw new Exception("Закончились подсказки");
            while (true)
            {
                int x = random.Next(0, 9);
                int y = random.Next(0, 9);
                if (this.userfield[x, y] == 0 || this.userfield[x, y] != this.trueField[x, y])
                {
                    this.userfield[x, y] = this.TrueField[x, y];
                    this.hints -= 1;
                    Check();
                    return new int[] { x, y };
                }

            }
        }
        private void SudokuGen() //генерация судоку
        {
            this.trueField = new int[9, 9];
            for(int i =0; i < 3; i++)
            {
                this.trueField = GenArea(this.trueField, i);
            }

            fillRemaining(this.trueField, 0, 0);
            this.field = this.trueField.Clone() as int[,];
            RemoveKDigits(this.level);
            this.userfield = this.field.Clone() as int[,];
            
        }

        private bool fillRemaining(int[,] grid, int i, int j)//заполняет все остальное
        {
            if (i == 9)
            {
                return true;
            }
            if (j == 9)
            {
                return fillRemaining(grid, i + 1, 0);
            }
            if (grid[i, j] != 0)
            {
                return fillRemaining(grid, i, j + 1);
            }
            for (int num = 1; num <= 9; num++)
            {
                if (IsFit(grid, i, j, num))
                {
                    this.trueField[i, j] = num;
                    if (fillRemaining(grid, i, j + 1))
                    {
                        return true;
                    }
                    this.trueField[i, j] = 0;
                }
            }
            return false;
        }

        private int[,] GenArea(int[,] field, int ind)//генерирует три квадрата по диагонали
        {
            
            for(int i =1; i<10; i++)
            {
                while (true)
                {
                    int x = this.random.Next(ind*3, ind * 3+3);
                    int y = this.random.Next(ind*3,ind*3+3);
                    if (field[x,y] == 0)
                    {
                        field[x,y] = i;
                        break;
                    }
                }
            }
            
            return field;
        }

        public bool IsFit(int[,] m, int x, int y, int dig)//проверяет подходит ли цифра
        {
            
            for(int i = 0; i<9; i++)
            {
                if (m[x, i] == dig)
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (m[i, y] == dig)
                {
                    return false;
                }
            }
            for(int i = x/3*3; i< x/3*3+2; i++)
            {
                for(int j = y/3*3; j< y/3*3+2; j++)
                {
                    if (m[i, j] == dig)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void RemoveKDigits(int k)//убирает определенное количество цифр
        {
            
            while (k > 0)
            {
                int cellId = this.random.Next(81);
                int i = cellId / 9;
                int j = cellId % 9;
                if (this.field[i, j] != 0)
                {
                    this.field[i, j] = 0;
                    k--;
                }
            }
        }

        private void Check()//проверка на победу
        {
            this.solved = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.userfield[i, j] == 0 || this.userfield[i, j] != this.trueField[i, j])
                        this.solved = false;
                }
            }
        }
    }

    
}
