using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class Program
    {
        static void readFromFile(string path)
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string str in lines)
                {
                    string[] one_note = str.Split('#');
                    foreach (string word in one_note)
                    {
                        Console.Write($"{word} ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Данный файл ещё не существует!");
            }
        }

        static void writeInFile(string path, string note)
        {
            if(!File.Exists(path))
            {
                File.WriteAllText(path, note);
            }
            else
            {
                File.AppendAllText(path, $"\n{note}");
            }
        }

        static string writeOneNote()
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("Введите ID записи в формате (1, 2, 3 ...):");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите дату и время " +
                "добавления записи в формате (дд.мм.гггг чч:мм):");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите Ф.И.О в формате (Иванов иван Иванович)");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите возраст в формате (25)");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите рост в формате (176)");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите дату рождения в формате (дд.мм.гггг)");
            sb.Append($"{Console.ReadLine()}#");
            Console.WriteLine("Введите место рождения в формате (город Москва)");
            sb.Append($"{Console.ReadLine()}");

            return sb.ToString();
        }

        static void Main(string[] args)
        {
            string path = "staff.txt";
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Введите 1, чтобы вывести данные " +
                    "из файла на экран\n" +
                    "Введите 2, чтобы ввести данные, которые добавятся " +
                    "новой записью в конец файла\n" +
                    "Введите 3, чтобы выйти из программы");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Вы неверно ввели число, попробуйте снова:");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        {
                            readFromFile(path);
                            break;
                        }
                        
                    case 2:
                        {
                            string note = writeOneNote();
                            writeInFile(path, note);
                            break;
                        }
                    case 3:
                        flag = false;
                        break;

                    default:
                        continue;
                }
            }
            

            Console.ReadKey(true);
        }
    }
}
