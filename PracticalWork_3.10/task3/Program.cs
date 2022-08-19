using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите целое число: ");
            int num = int.Parse(Console.ReadLine());
            
            bool flag = false;
            int range = num / 2 + 1;
            for (int i = 2; i < range; i++)
            {
                if (num % i == 0)
                {
                    flag = true;
                    break;
                }
            }
            if(flag)
                Console.WriteLine($"Число {num} это непростое число");
            else
                Console.WriteLine($"Число {num} это простое число");

            Console.ReadKey(true);
        }
    }
}
