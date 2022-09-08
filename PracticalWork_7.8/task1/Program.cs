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
            DateTime note_date, birth_date;
            string fio, birth_place;
            uint age, height;
            while (true)
            {
                Console.WriteLine("Создание сотрудника:");

                Console.WriteLine("Введите дату и время " +
                    "добавления записи в формате (дд.мм.гггг чч:мм):");
                if(!DateTime.TryParse(Console.ReadLine(), out note_date))
                {
                    Console.WriteLine("Вы ввели неверную дату записи, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите Ф.И.О в формате (Иванов иван Иванович)");
                fio = Console.ReadLine();
                
                Console.WriteLine("Введите возраст в формате (25)");
                if (!uint.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Вы некорректно ввели возраст, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите рост в формате (176)");
                if (!uint.TryParse(Console.ReadLine(), out height))
                {
                    Console.WriteLine("Вы некорректно ввели рост, попробуйте снова:\n");
                    continue;
                }

                Console.WriteLine("Введите дату рождения в формате (дд.мм.гггг)");
                if (!DateTime.TryParse(Console.ReadLine(), out birth_date))
                {
                    Console.WriteLine("Вы ввели неверную дату рождения, попробуйте снова:\n");
                    continue;
                }
                Console.WriteLine("Введите место рождения в формате (город Москва)");
                birth_place = Console.ReadLine();

                break;
            }

            return new Worker(0, note_date, fio, age, height, birth_date, birth_place);
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

            rep.AddWorker(new Worker(0, DateTime.Now,
                "Иванов И.С.", 26, 182, DateTime.Parse("5.8.1996"), "Томск"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 9, 4, 16, 30, 20),
                "Петров М.В.", 29, 174, DateTime.Parse("4.5.1993"), "Уфа"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 7, 17, 12, 33, 10),
                "Сидоров А.Н.", 40, 177, DateTime.Parse("4.5.1982"), "Москва"));
            rep.AddWorker(new Worker(0, new DateTime(2022, 5, 12, 13, 10, 15),
                "Максимова А.С.", 34, 168, DateTime.Parse("4.5.1988"), "Пермь"));

            #endregion

            bool flag = true;
            while(flag)
            {
                Console.WriteLine("Выберите одно из следующих" +
                    " действий по базе сотрудников:\n" +
                    "1) Введите 1, чтобы получить данные о базе в консоль\n" +
                    "2) Введите 2, чтобы добавить запись в базу\n" +
                    "3) Введите 3, чтобы удалить запись из базы\n" +
                    "4) Введите 4, чтобы получить определённую запись из базы\n" +
                    "5) Введите 5, чтобы получить диапазон записей из базы " +
                    "между двумя датами\n" +
                    "6) Введите 6, чтобы получить отсортированные записи " +
                    "из базы по какой-либо характеристике\n" +
                    "7) Введите 0, чтобы выйти из программы, " +
                    "если закончили свою работу");

                int choice;
                // Сохраняем выбор из меню в переменную, строго проверяя на корректность ввода
                while(true)
                {
                    while(!int.TryParse(Console.ReadLine(), out choice))
                        Console.WriteLine("Вы ошиблись при вводе, повторите попытку");
                    if (choice >= 0 && choice <= 6)
                        break;
                    else
                        Console.WriteLine("Вы ошиблись при вводе числа, повторите попытку");
                }   

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Выход из Приложения!");
                        flag = false;
                        break;
                    case 1:
                        {
                            //Получение всех записей из файла
                            Console.WriteLine("\nПолучение всех записей из базы");
                            Worker[] workers = rep.GetAllWorkers();
                            PrintWorkers(workers);
                            Console.WriteLine();
                            break;
                        }
                            
                    case 2:
                        {
                            // Добавление записи
                            Console.WriteLine();
                            rep.AddWorker(CreateOneWorker());
                            Console.WriteLine("Запись добавлена!");
                            Console.WriteLine();
                            break;
                        }
                            
                    case 3:
                        {
                            // Удаление записи
                            Console.WriteLine("\nУдаление записи");
                            Console.WriteLine("Введите индекс записи, которую хотите удалить:");
                            while (!int.TryParse(Console.ReadLine(), out choice))
                                Console.WriteLine("Вы ввели не число, а символ, повторите попытку");
                            rep.DeleteWorker(choice);
                            Console.WriteLine();
                            break;
                        }
                        
                    case 4:
                        {
                            // Получение определённой записи из файла
                            Console.WriteLine("\nПолучение определённой записи из файла");
                            Console.WriteLine("Введите индекс записи, которую хотите получить:");
                            while (!int.TryParse(Console.ReadLine(), out choice))
                                Console.WriteLine("Вы ввели не число, а символ, повторите попытку");
                            Worker worker = rep.GetWorkerById(choice);
                            if(worker.IsEmpty())
                                Console.WriteLine("Записи по такому id не существует!");
                            else
                                worker.Print();
                            Console.WriteLine();
                            break;
                        }
                        
                    case 5:
                        {
                            // Получение диапазона записей между двумя датами из файла
                            Console.WriteLine("\nПолучение диапазона записей между двумя датами из файла");
                            DateTime date_from, date_to;
                            Console.WriteLine("Введите дату и время добавления записи в формате " +
                                "(дд.мм.гггг чч:мм), как начало диапазона");
                            while (!DateTime.TryParse(Console.ReadLine(), out date_from))
                                Console.WriteLine("Вы ввели неверную дату записи, попробуйте снова:");

                            Console.WriteLine("Введите дату и время добавления записи в формате " +
                                "(дд.мм.гггг чч:мм), как конец диапазона");
                            while (!DateTime.TryParse(Console.ReadLine(), out date_to))
                                Console.WriteLine("Вы ввели неверную дату записи, попробуйте снова:");

                            Worker[] workers = rep.GetWorkersBetweenTwoDates(
                                date_from,
                                date_to);
                            PrintWorkers(workers);
                            Console.WriteLine();
                            break;
                        }
                        
                    case 6:
                        {
                            // Получение отсортированных записей по характеристике из файла
                            Console.WriteLine("\nПолучение отсортированных записей по характеристике из файла\n" +
                                "Введите число от 0 до 6, где\n" +
                                "0 - сортировка по id\n" +
                                "1 - сортировка по дате добавления записи\n" +
                                "2 - сортировка по именам\n" +
                                "3 - сортировка по возрасту\n" +
                                "4 - сортировка по росту\n" +
                                "5 - сортировка по дате рождения\n" +
                                "6 - сортировка по месту рождения\n" +
                                "При вводе чего-то другого из чисел, сортировка не произойдёт, " +
                                "вы увидите записи в базе как они есть");
                            while (!int.TryParse(Console.ReadLine(), out choice))
                                Console.WriteLine("Вы ввели не число, а символ, повторите попытку");
                            Worker[] sorted_workers = rep.OrderByField(choice);
                            Console.WriteLine();
                            PrintWorkers(sorted_workers);
                            Console.WriteLine();
                            break;
                        }
                }
            }
            Console.ReadKey(true);
        }
    }
}
