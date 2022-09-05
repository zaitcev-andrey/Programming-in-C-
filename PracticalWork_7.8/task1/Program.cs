using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    internal class Program
    {
        /// <summary>
        /// Создание работника по данным пользователя
        /// </summary>
        /// <returns>Экземпляр работника</returns>
        static Worker CreateOneWorker()
        {
            Worker worker = new Worker();
            while(true)
            {
                Console.WriteLine("Создание работника:");

                Console.WriteLine("Введите дату и время " +
                    "добавления записи в формате (дд.мм.гггг чч:мм):");
                if(!DateTime.TryParse(Console.ReadLine(), out DateTime note_date))
                {
                    Console.WriteLine("Вы ввели неверную дату записи, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите Ф.И.О в формате (Иванов иван Иванович)");
                string fio = Console.ReadLine();
                
                Console.WriteLine("Введите возраст в формате (25)");
                if (!int.TryParse(Console.ReadLine(), out int age))
                {
                    Console.WriteLine("Вы некорректно ввели возраст, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите рост в формате (176)");
                if (!int.TryParse(Console.ReadLine(), out int height))
                {
                    Console.WriteLine("Вы некорректно ввели рост, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите дату рождения в формате (дд.мм.гггг)");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime birth_date))
                {
                    Console.WriteLine("Вы ввели неверную дату рождения, попробуйте снова:\n");
                    continue;
                }
                Console.WriteLine("Введите место рождения в формате (город Москва)");
                string birth_place = Console.ReadLine();

                worker.Note_date = note_date;
                worker.Fio = fio;
                worker.Age = age;
                worker.Height = height;
                worker.Birth_date = birth_date;
                worker.Birth_place = birth_place;
                break;
            }
            return worker;
        }

        /// <summary>
        /// Распечатка данных каждого работника в массиве работников
        /// </summary>
        /// <param name="workers">Массив работников</param>
        static void PrintWorkers(Worker[] workers)
        {
            foreach (Worker worker in workers)
            {
                worker.Print();
            }
        }
        static void Main(string[] args)
        {
            // Создание объекта репозитория, через который можно работать с файлом
            Repository rep = new Repository("workers.txt");

            #region Добавление записей в файл

            //rep.AddWorker(CreateOneWorker());
            rep.AddWorker(new Worker(0, DateTime.Now,
                "Иванов И.С.", 26, 182, DateTime.Parse("5.8.1996"), "Томск"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 9, 4, 16, 30, 20),
                "Петров М.В.", 29, 174, DateTime.Parse("4.5.1993"), "Уфа"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 7, 17, 12, 33, 10),
                "Сидоров А.Н.", 40, 177, DateTime.Parse("4.5.1982"), "Москва"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 5, 12, 13, 10, 15),
                "Максимова А.С.", 34, 168, DateTime.Parse("4.5.1988"), "Пермь"));

            #endregion

            #region Получение данных из файла и их распечатка в консоле
            //Получение всех записей из файла
            Console.WriteLine("Получение всех записей из файла");
            Worker[] workers = rep.GetAllWorkers();
            PrintWorkers(workers);
            Console.WriteLine();
            Console.ReadKey(true);

            // Получение определённой записи из файла
            Console.WriteLine("Получение определённой записи из файла");
            Worker worker1 = rep.GetWorkerById(2);
            worker1.Print();
            Console.WriteLine();
            Console.ReadKey(true);

            // Получение диапазона записей между двумя датами из файла
            Console.WriteLine("Получение диапазона записей между двумя датами из файла");
            Worker[] workers2 = rep.GetWorkersBetweenTwoDates(
                new DateTime(2022, 6, 7, 11, 20, 20),
                new DateTime(2022, 9, 5, 11, 20, 20));
            PrintWorkers(workers2);
            Console.WriteLine();
            Console.ReadKey(true);

            // Получение отсортированных записей по полю из файла
            Console.WriteLine("Получение отсортированных записей по полю из файла");
            Worker[] sorted_workers = rep.OrderByField(4); // 4 - рост
            PrintWorkers(sorted_workers);
            Console.WriteLine();
            Console.ReadKey(true);
            #endregion

            #region Удаление записей и распечатка данных
            Console.WriteLine("Удаление записей. Это исходный массив до удаления");
            rep.Print();
            Console.ReadKey(true);
            Console.WriteLine("Удалили запись по индексу 1");
            rep.DeleteWorker(1);
            rep.Print();
            Console.ReadKey(true);
            Console.WriteLine("Удалили запись по индексу 2");
            rep.DeleteWorker(2);
            rep.Print();
            #endregion

            Console.ReadKey(true);
        }
    }
}
