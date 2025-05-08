using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuV1
{
    public class Data
    {
        public string folder= "C:/Users/daram/OneDrive/Документы/SudokuApp";
        public string file;

        public Data(string name)
        {
            Directory.CreateDirectory(folder);
            file = Path.Combine(folder, name+".txt");
            if (!File.Exists(file))
            {
                File.Create(file).Close(); // сразу закрываем stream
            }
        }

        public string Read()
        {
            string text = File.ReadAllText(file);
            return text;
        }
        public void WriteSudoku(int[,] field)
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    File.AppendAllText(file, field[i,j]+" ");
                File.AppendAllText(file, "\n");
            }
        }
        public void WriteString(string str)
        {
                File.AppendAllText(file, str+"\n");
            
        }
        public void DeleteStr()
        {
            File.WriteAllText(file, "");
        }
    }
}
