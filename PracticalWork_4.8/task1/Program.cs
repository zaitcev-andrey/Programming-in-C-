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
            Console.WriteLine("Задание 1. Случайная матрица\n");

            int rows, cols;

            while(true)
            {
                Console.Write("Введите количество строк в матрице: ");
                if (!int.TryParse(Console.ReadLine(), out rows))
                {
                    Console.WriteLine("Вы ошиблись при вводе числа, попробуйте снова!");
                    continue;
                }
                if(rows <= 0)
                {
                    Console.WriteLine("Число строк это отрицательное число или ноль," +
                        " такого быть не может, попробуйте снова!");
                    continue;
                }

                Console.Write("Введите количество столбцов в матрице: ");
                if (!int.TryParse(Console.ReadLine(), out cols))
                {
                    Console.WriteLine("Вы ошиблись при вводе числа, попробуйте снова!");
                    continue;
                }
                if (cols <= 0)
                {
                    Console.WriteLine("Число столбцов это отрицательное число или ноль," +
                        " такого быть не может, попробуйте снова!");
                    continue;
                }
                break;
            }

            int[,] matrix = new int[rows, cols];
            Random random = new Random();
            int elementSum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(11);
                    elementSum += matrix[i, j];
                    Console.Write($"{matrix[i, j],3}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nСумма всех элементов матрицы = {elementSum}");

            Console.ReadKey(true);
        }
    }
}
