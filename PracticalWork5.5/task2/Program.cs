using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    internal class Program
    {
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

        static string ReverseWords(string inputPhrase)
        {
            string[] subs = MyGetSubStrings(inputPhrase);
            string result = "";
            for (int i = subs.Length - 1; i >= 0; i--)
            {
                result += subs[i] + " ";
            }
            // выбираем подстроку без последнего пробела
            string new_result = result.Substring(0, result.Length - 1);
            return new_result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку из нескольких слов:");
            string str = Console.ReadLine();
            string reverseStr = ReverseWords(str);
            Console.WriteLine($"Эта же строка в обратной последовательности:\n{ reverseStr}");
        }
    }
}
