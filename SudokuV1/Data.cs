
using System.IO;


namespace SudokuV1
{
    public class Data
    {
        public string folder= ""; //папка для сохранения
        public string file; //путь к созданному файлу

        public Data(string name)
        {
            Directory.CreateDirectory(folder);
            file = Path.Combine(folder, name+".txt");
            if (!File.Exists(file))
            {
                File.Create(file).Close(); // сразу закрываем stream
            }
        }

        public string Read() //читаем из файла
        {
            string text = File.ReadAllText(file);
            return text;
        }
        public void WriteSudoku(int[,] field) //записывает судоку в файл
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    File.AppendAllText(file, field[i,j]+" ");
                File.AppendAllText(file, "\n");
            }
        }
        public void WriteString(string str)//записывает строку в файл
        {
                File.AppendAllText(file, str+"\n");
            
        }
        public void DeleteStr()//удаляет все из файла
        {
            File.WriteAllText(file, "");
        }
    }
}
