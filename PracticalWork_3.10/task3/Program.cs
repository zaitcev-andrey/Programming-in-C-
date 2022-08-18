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
            while (!flag)
            {
                for (int i = 2; i < num; i++)
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
                {
                    Console.WriteLine($"Число {num} это простое число");
                    flag = true;
                }
            }
            Console.ReadKey(true);
        }
    }
}
