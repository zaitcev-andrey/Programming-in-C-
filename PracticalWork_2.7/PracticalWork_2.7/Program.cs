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

            string pattern = "Ф.И.О: {0} \nВозраст: {1} \nПочта: {2} \nБаллы по программированию: {3} \nБаллы по математике: {4} \nБаллы по физике: {5}";

            Console.WriteLine(pattern,
                fullName,
                age,
                email,
                programmScores,
                mathScores,
                physicsScores);
        }
    }
}
