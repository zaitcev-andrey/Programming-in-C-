using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task3_OOP1_Console
{
    internal class Program
    {
        static void PrintClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                client.PrintFio();
            }
        }

        static void AddNoteInJsonString(List<Client> clients, IManagerMethods manager)
        {
            clients.Add(manager.AddNewNoteAboutClient());
            string json = JsonConvert.SerializeObject(clients); // clients[clients.Count - 1] только для последней записи
            File.WriteAllText("clients.json", json);
        }
        static void Main(string[] args)
        {
            // Тут мы подгружаем данные из файла json, а если его нет (как например при первом запуске,
            // то мы создайм эти данные и одновременно с этим записываем в файл json для следующих запусков)
            List<Client> clients = new List<Client>();
            if (File.Exists("clients.json"))
            {
                string json = File.ReadAllText("clients.json");
                clients = JsonConvert.DeserializeObject<List<Client>>(json);
            }
            else
            {
                Client cl1 = new Client("Иван", "Иванов", "Иванович", "89998762315", "7718 999888");
                Client cl2 = new Client("Сергей", "Сергеев", "Сергеевич", "87778762315", "3466 999888");
                Client cl3 = new Client("Пётр", "Петров", "Петрович", "85558762315", "1922 999888");
                Client cl4 = new Client("Александра", "Сидорова", "Артёмовна", "83338762315", "9813 999888");
                clients.Add(cl1);
                clients.Add(cl2);
                clients.Add(cl3);
                clients.Add(cl4);
                string json = JsonConvert.SerializeObject(clients);
                File.WriteAllText("clients.json", json);
            }
            // Проведём тесты по работоспособности консультанта и менеджера

            Console.WriteLine("Введите 1, если вы консультант, или 2, если вы менеджер, " +
                "чтобы получить доступ к своему функционалу.\n" +
                "Если хотите выйти из программы, то введите что-то другое");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Вы вошли в режим консультанта. Вам доступен просмотр данных клиента, \n" +
                            "кроме его паспорта. Также вы можете изменить номер клиента.");
                        Consultant consultant = new Consultant();
                        PrintClients(clients);
                        clients[1].CheckChanges();
                        Console.WriteLine($"Номер телефона: {consultant.GetClientNumber(clients[1])}");
                        consultant.SetClientTelephoneNumber(clients[1]);
                        Console.WriteLine($"Номер телефона: {consultant.GetClientNumber(clients[1])}");
                        clients[1].CheckChanges();

                        consultant.PrintCLientPasportData(clients[0]);
                        break;
                    }

                case "2":
                    {
                        Console.WriteLine("Вы вошли в режим менеджера. Вам доступны просмотр и измененение всех данных клиента");
                        Manager manager = new Manager();
                        PrintClients(clients);
                        clients[0].CheckChanges();
                        Console.WriteLine("1) Протестируем работу с изменением ФИО по отдельности");
                        manager.PrintClientFIO(clients[0]);
                        manager.SetClientFirstName(clients[0]);
                        manager.SetClientLastName(clients[0]);
                        manager.SetClientMiddleName(clients[0]);
                        manager.PrintClientFIO(clients[0]);
                        clients[0].CheckChanges();

                        Console.WriteLine("2) Протестируем работу с получением и изменением ФИО целиком");
                        manager.PrintClientFIO(clients[0]);
                        manager.SetClientFio(clients[0]);
                        manager.PrintClientFIO(clients[0]);
                        clients[0].CheckChanges();

                        Console.WriteLine("3) Протестируем работу с получением и изменением номера телефона");
                        Console.WriteLine($"Номер телефона: {manager.GetClientNumber(clients[0])}");
                        manager.SetClientTelephoneNumber(clients[0]);
                        Console.WriteLine($"Номер телефона: {manager.GetClientNumber(clients[0])}");
                        clients[0].CheckChanges();

                        Console.WriteLine("4) Протестируем работу с получением и изменением данных паспорта");
                        manager.PrintCLientPasportData(clients[0]);
                        manager.SetClientPasportData(clients[0]);
                        manager.PrintCLientPasportData(clients[0]);
                        clients[0].CheckChanges();

                        Console.WriteLine("5) Протестируем работу с добавлением новой записи о клиенте");
                        AddNoteInJsonString(clients, manager);
                        //clients.Add(manager.AddNewNoteAboutClient());
                        clients[clients.Count - 1].CheckChanges();

                        Console.WriteLine("\nПокажем всех клиентов:");
                        PrintClients(clients);
                        break;
                    }

                default:
                    Console.WriteLine("Выход из программы, до свидания!");
                    break;
            }

            Console.ReadKey(true);
        }
    }
}
