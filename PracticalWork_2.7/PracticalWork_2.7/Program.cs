using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalWork_2._7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fullName = "Ivanov Ivan Ivanovich";
            int age = 22;
            string email = "myEmail@gmail.com";
            double programmScores = 4.2;
            double mathScores = 4.6;
            double physicsScores = 4.4;

            string pattern = "Ф.И.О: {0} \n" +
                "Возраст: {1} \n" +
                "Почта: {2} \n" +
                "Баллы по программированию: {3} \n" +
                "Баллы по математике: {4} \n" +
                "Баллы по физике: {5}";

            Console.WriteLine(pattern,
                fullName,
                age,
                email,
                programmScores,
                mathScores,
                physicsScores);

            Console.ReadKey(true);
            double scoresSum = programmScores + mathScores + physicsScores;
            double arithmeticMean = scoresSum / 3;
            Console.WriteLine($"\nСреднее арифметическое баллов = {arithmeticMean}");

            Console.ReadKey(true);
        }
    }
}
