using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число: ");
            int number = int.Parse(Console.ReadLine());
            if (number % 2 == 0)
                Console.WriteLine($"Число {number} чётное");
            else
                Console.WriteLine($"Число {number} нечётное");
            Console.ReadKey(true);
        }
    }
}
