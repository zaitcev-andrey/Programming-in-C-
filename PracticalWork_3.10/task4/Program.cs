using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int minValue = int.MaxValue;
            Console.Write("Введите длину последовательности чисел: ");
            int length = int.Parse(Console.ReadLine());
            for (int i = 1; i <= length; i++)
            {
                Console.Write($"Введите число {i}: ");
                int value = int.Parse(Console.ReadLine());
                if(value < minValue)
                    minValue = value;
            }
            Console.WriteLine($"Минимальное значение в вашей " +
                $"последовательности = {minValue}");

            Console.ReadKey(true);
        }
    }
}
