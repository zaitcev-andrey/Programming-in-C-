using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    internal class Program
    {
        /// <summary>
        /// Заполнение словаря данными
        /// </summary>
        /// <param name="map"></param>
        static void FillDictionary(Dictionary<string, string> map)
        {
            while (true)
            {
                Console.WriteLine("Если хотите выйти, " +
                    "то отправьте пустую строку, иначе введите номер телефона:");
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key))
                    break;
                Console.WriteLine("Отлично, теперь введите ФИО владельца телефона:");
                string value = Console.ReadLine();
                map[key] = value;
                Console.WriteLine("Данные сохранены, можете добавить следующие");
            }
        }

        /// <summary>
        /// Поиск в словаре определённого ключа и в случае успеха печать значения
        /// </summary>
        /// <param name="map"></param>
        static void FindAndPrintValueInDictionaryByKey(Dictionary<string, string> map)
        {
            Console.WriteLine("А теперь давайте найдём владельца по телефону\n" +
                "Введите номер телефона, владельца которого хотите получить:");
            string key = Console.ReadLine();
            if (map.TryGetValue(key, out string value))
                Console.WriteLine($"ФИО владельца: {value}");
            else Console.WriteLine("Извините, но владельца с таким номером нет!");
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа записи телефонов и их владельцев\n");
            Dictionary<string, string> map = new Dictionary<string, string>();
            
            FillDictionary(map);
            FindAndPrintValueInDictionaryByKey(map);

            Console.ReadKey(true);
        }
    }
}
