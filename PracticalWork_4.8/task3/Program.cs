using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{

    public class LifeSimulation
    {
        private int _heigth;
        private int _width;
        private bool[,] cells;

        /// <summary>
        /// Создаем новую игру
        /// </summary>
        /// <param name="Heigth">Высота поля.</param>
        /// <param name="Width">Ширина поля.</param>

        public LifeSimulation(int Heigth, int Width)
        {
            _heigth = Heigth;
            _width = Width;
            cells = new bool[Heigth, Width];
            GenerateField();
        }

        /// <summary>
        /// Перейти к следующему поколению и вывести результат на консоль.
        /// </summary>
        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        /// <summary>
        /// Двигаем состояние на одно вперед, по установленным правилам
        /// </summary>

        private void Grow()
        {
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    int numOfAliveNeighbors = GetNeighbors(i, j);

                    if (cells[i, j])
                    {
                        if (numOfAliveNeighbors < 2)
                        {
                            cells[i, j] = false;
                        }

                        if (numOfAliveNeighbors > 3)
                        {
                            cells[i, j] = false;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            cells[i, j] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Смотрим сколько живых соседий вокруг клетки.
        /// </summary>
        /// <param name="x">X-координата клетки.</param>
        /// <param name="y">Y-координата клетки.</param>
        /// <returns>Число живых клеток.</returns>

        private int GetNeighbors(int x, int y)
        {
            int NumOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= _heigth || j >= _width)))
                    {
                        if (cells[i, j] == true) NumOfAliveNeighbors++;
                    }
                }
            }
            return NumOfAliveNeighbors;
        }

        /// <summary>
        /// Нарисовать Игру в консоле
        /// </summary>

        private void DrawGame()
        {
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write(cells[i, j] ? "x" : " ");
                    if (j == _width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        /// <summary>
        /// Инициализируем случайными значениями
        /// </summary>

        private void GenerateField()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    number = generator.Next(2);
                    cells[i, j] = ((number == 0) ? false : true);
                }
            }
        }
    }
}

namespace task3
{
    internal class Program
    {
        // Ограничения игры
        private const int Heigth = 10;
        private const int Width = 30;
        private const uint MaxRuns = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("Задание 3. Игра 'Жизнь'\n");

            #region код из примера
            //int runs = 0;
            //GameOfLife.LifeSimulation sim = new GameOfLife.LifeSimulation(Heigth, Width);

            //while (runs++ < MaxRuns)
            //{
            //    sim.DrawAndGrow();

            //    // Дадим пользователю шанс увидеть, что происходит, немного ждем
            //    System.Threading.Thread.Sleep(100);
            //}
            #endregion

            Console.CursorVisible = false;

            bool[,] bacterias = new bool[Heigth, Width];
            int[,] lifeDuration = new int[Heigth, Width];
            Random generator = new Random();
            int value;
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    value = generator.Next(2);
                    bacterias[i, j] = (value == 0) ? false : true;
                }
            }

            #region Текст задачи
            Console.WriteLine("Бактерии живут в синергии на своих ярусах\n" +
                "Если бактерия есть, она обозначается символом 'x', а если " +
                "её нет, то на её месте пробел (символ ' ')\n" +
                "С каждой итерацией погибает ровно половина бактерий на ярусе, а " +
                "затем столько же появляется в случайных местах\n" +
                "Если бактерия смогла прожить 5 итераций, она становится " +
                "Альфа-бактерией (символ 'O') и уже не может погибнуть\n" +
                "Со временем Все бактерии смогут стать Альфа-бактериями, " +
                "тем самым заполонят всё поле (но всё зависит от кол-ва итераций)");
            #endregion
            
            int runs = 0;
            while(runs++ < MaxRuns)
            {
                for (int i = 0; i < Heigth; i++)
                {
                    Console.SetCursorPosition(0, Console.WindowTop + 10 + i); // отрисовываем в нужном нам месте
                    for (int j = 0; j < Width; j++)
                    {
                        if(bacterias[i, j])
                        {
                            Console.Write((lifeDuration[i, j] >= 5) ? " O " : " x ");
                        }
                        else Console.Write("   ");
                        if (j == Width - 1)
                        {
                            Console.WriteLine("\r");
                        }
                    }
                }
                System.Threading.Thread.Sleep(500);

                for (int i = 0; i < Heigth; i++)
                {
                    int quantityOfLifes = 0;
                    for (int j = 0; j < Width; j++)
                    {
                        if (bacterias[i, j])
                        {
                            quantityOfLifes++;
                            lifeDuration[i, j]++;
                        }
                    }
                    int deaths = quantityOfLifes / 2;
                    int whichToKill;
                    for (int j = 0; j < deaths; j++)
                    {
                        whichToKill = generator.Next(0, quantityOfLifes - j);
                        int deathInd = -1;
                        for (int k = 0; k < Width; k++)
                        {
                            if (bacterias[i,k])
                                deathInd++;
                            if (deathInd == whichToKill)
                            {
                                if (lifeDuration[i, k] >= 5)
                                    break;
                                else
                                {
                                    bacterias[i, k] = false;
                                    lifeDuration[i, k] = 0;
                                }
                            }
                            
                        }
                    }

                    int birth = deaths;
                    int whichToBeBorn;
                    int quantityOfDeaths = Width - quantityOfLifes + deaths;
                    for (int j = 0; j < birth; j++)
                    {
                        whichToBeBorn = generator.Next(0, quantityOfDeaths - j);
                        int birthInd = -1;
                        for (int k = 0; k < Width; k++)
                        {
                            if (!bacterias[i, k])
                                birthInd++;
                            if (birthInd == whichToBeBorn)
                                bacterias[i, k] = true;  
                        }
                    }
                }
            }


            Console.ReadKey(true);
        }
    }
}
