using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 2. Сложение матриц\n");

            int rows, cols;

            while (true)
            {
                Console.Write("Введите количество строк в матрице: ");
                if (!int.TryParse(Console.ReadLine(), out rows))
                {
                    Console.WriteLine("Вы ошиблись при вводе числа, попробуйте снова!");
                    continue;
                }
                if (rows <= 0)
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

            int[,] matrixA = new int[rows, cols];
            int[,] matrixB = new int[rows, cols];

            Random random = new Random();
            Console.WriteLine("Значения первой матрицы:");
            for (int i = 0; i < matrixA.GetLength(0); i++)
            {
                for (int j = 0; j < matrixA.GetLength(1); j++)
                {
                    matrixA[i, j] = random.Next(11);
                    Console.Write($"{matrixA[i, j],3}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nЗначения второй матрицы:");
            for (int i = 0; i < matrixB.GetLength(0); i++)
            {
                for (int j = 0; j < matrixB.GetLength(1); j++)
                {
                    matrixB[i, j] = random.Next(11);
                    Console.Write($"{matrixB[i, j],3}");
                }
                Console.WriteLine();
            }

            int[,] matrixC = new int[rows, cols];
            Console.WriteLine("\nЗначения третьей результирующей матрицы:");
            for (int i = 0; i < matrixC.GetLength(0); i++)
            {
                for (int j = 0; j < matrixC.GetLength(1); j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                    Console.Write($"{matrixC[i, j],3}");
                }
                Console.WriteLine();
            }

            Console.ReadKey(true);
        }
    }
}
