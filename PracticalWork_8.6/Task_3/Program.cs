using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    internal class Program
    {
        /// <summary>
        /// Заполнение множества только уникальными числами
        /// и вывод сообщения о попытке добавить дубликат
        /// </summary>
        /// <param name="set"></param>
        static void WorkWithHashSet(HashSet<int> set)
        {
            while (true)
            {
                Console.WriteLine("Если хотите выйти из программы, " +
                    "то отправьте пустую строку, иначе введите число:");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;
                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Вы ввели не число, попробуйте снова," +
                        " но будьте аккуратнее!");
                    continue;
                }
                if (set.Contains(value))
                    Console.WriteLine("Число не сохранено, так как уже вводилось ранее!");
                else
                {
                    set.Add(value);
                    Console.WriteLine("Число успешно сохранено!");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует программа, которая будет сохранять" +
                " только уникальные числа\n");
            HashSet<int> set = new HashSet<int>();
            
            WorkWithHashSet(set);

            Console.ReadKey(true);
        }
    }
}
