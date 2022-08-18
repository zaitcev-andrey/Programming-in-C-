using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите максимальное целое число диапазона: ");
            int maxRange = int.Parse(Console.ReadLine());
            Random random = new Random();
            int randValue = random.Next(maxRange + 1);

            Console.Write("Попробуйте отгадать загаданное число." +
                " Если устанете играть, отправьте пустую строку." +
                    " Введите число: ");
            string str;
            int ourValue;
            int count = 0;
            while (true)
            {
                str = Console.ReadLine();
                if (String.IsNullOrEmpty(str))
                {
                    Console.WriteLine($"Жаль, что вы не угадали." +
                        $" Это было число: {randValue}");
                    break;
                }
                ourValue = int.Parse(str);
                count++;
                if (ourValue > randValue)
                    Console.Write("Ваше число больше загаданного," +
                        " ищите дальше: ");
                else if(ourValue < randValue)
                    Console.Write("Ваше число меньше загаданного," +
                        " ищите дальше: ");
                else
                {
                    Console.WriteLine($"Вы угадали число, это {randValue}");
                    Console.WriteLine($"Вы угадали с {count} раза!");
                    break;
                }

            }
            
            Console.ReadKey(true);
        }
    }
}
