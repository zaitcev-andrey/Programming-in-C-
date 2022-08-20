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
            Console.WriteLine("Привет дорогой друг, " +
                "сколько карт у тебя на руках? Введи это число:");
            int num = int.Parse(Console.ReadLine());
            
            int sum = 0;
            bool flag = true;
            while (flag)
            {
                sum = 0;
                Console.WriteLine($"Сейчас вам нужно будет ввести" +
                $" номинал для каждой карты. " +
                $"Если это число, вводите от 2 до 10 " +
                $"включительно.\nЕсли это картинка, то вводите так: " +
                $"Валет = J, Дама = Q, Король = K, Туз = T");
                for (int i = 1; i <= num; i++)
                {
                    Console.Write($"Введите номинал {i} карты: ");
                    string str = Console.ReadLine();

                    if (int.TryParse(str, out int nominal))
                    {
                        switch (nominal)
                        {
                            case int x when x >= 2 && x <= 10:
                                sum += x;
                                break;
                            default:
                                Console.WriteLine("\nВы неверно ввели номинал карты, попробуйте снова!\n");
                                flag = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (str)
                        {
                            case "J":
                            case "Q":
                            case "K":
                            case "T":
                                sum += 10;
                                break;
                            default:
                                Console.WriteLine("\nВы неверно ввели номинал карты, попробуйте снова!\n");
                                flag = false;
                                break;
                        }
                    }

                    //switch (str)
                    //{
                    //    case "2":
                    //    case "3":
                    //    case "4":
                    //    case "5":
                    //    case "6":
                    //    case "7":
                    //    case "8":
                    //    case "9":
                    //    case "10":
                    //        sum += int.Parse(str);
                    //        break;
                    //    case "J":
                    //    case "Q":
                    //    case "K":
                    //    case "T":
                    //        sum += 10;
                    //        break;
                    //    default:
                    //        Console.WriteLine("\nВы неверно ввели номинал карты, попробуйте снова!\n");
                    //        flag = false;
                    //        break;
                    //}
                    if (!flag)
                        break;
                }
                if (!flag)
                    flag = true;
                else
                    flag = false;
            }

            Console.WriteLine($"\nСумма из {num} карт равна: {sum}");
            Console.ReadKey(true);
        }
    }
}
