using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Program
    {
        /// <summary>
        /// Заполнение списка 100 случайными числами
        /// в диапазоне от 0 до 100
        /// </summary>
        /// <param name="list">Список целых элементов</param>
        static void FillList(List<int> list)
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
                list.Add(random.Next(0, 101));
        }

        /// <summary>
        /// Печать элементов списка
        /// </summary>
        /// <param name="list">Список целых элементов</param>
        static void PrintList(List<int> list)
        {
            int enter_count = 1;
            foreach (int item in list)
            {
                if(enter_count > 20)
                {
                    enter_count = 1;
                    Console.WriteLine();
                }
                Console.Write($"{item} ");
                enter_count++;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Удаление элементов в списке, больших 25 и меньших 50
        /// </summary>
        /// <param name="list">Список целых элементов</param>
        static void DeleteElementsFromList(List<int> list)
        {
            // Используем внутри предикат через лямбда-выражение
            list.RemoveAll(item => item > 25 && item < 50);
        }

        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            FillList(list);
            Console.WriteLine("Исходный список элементов");
            PrintList(list);
            DeleteElementsFromList(list);
            Console.WriteLine("\nСписок элементов после удаления");
            PrintList(list);

            Console.ReadKey(true);
        }
    }
}
