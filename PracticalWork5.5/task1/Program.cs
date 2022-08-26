using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class Program
    {
        static string[] GetSubStrings(string s)
        {
            return s.Split(' ');
        }

        static string[] MyGetSubStrings(string s)
        {
            List<string> list = new List<string>();
            int counter = 0;
            bool flag = false;
            string str = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                    continue;

                if (i != s.Length - 1)
                {
                    if (s[i + 1] == ' ')
                        flag = true;
                }
                else flag = true;


                str += s[i];

                if (flag == true)
                {
                    list.Add(str);
                    counter++;
                    flag = false;
                    str = "";
                    continue;
                }
            }
            string[] subs = new string[list.Count];
            for (int i = 0; i < subs.Length; i++)
            {
                subs[i] = list[i];
            }
            return subs;
        }

        static void PrintSubStrings(string[] subs)
        {
            foreach(string sub in subs)
            {
                Console.WriteLine(sub);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку из нескольких слов:");
            string s = Console.ReadLine();
            string[] subs = MyGetSubStrings(s);
            Console.WriteLine("\nРаспечатаем по отдельным словам:\n");
            PrintSubStrings(subs);
        }
    }
}
