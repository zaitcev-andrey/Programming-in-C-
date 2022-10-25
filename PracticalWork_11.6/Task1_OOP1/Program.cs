using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Task1_OOP1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Тут мы подгружаем данные из файла json, а если его нет (как например при первом запуске,
            // то мы создайм эти данные и одновременно с этим записываем в файл json для следующих запусков)
            List<Client> clients = new List<Client>();
            if(File.Exists("clients.json"))
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
            // Проведём тесты по работоспособности консультанта
            Consultant consultant = new Consultant();
            foreach (var client in clients)
            {
                consultant.PrintClientFIO(client);
            }
            Console.WriteLine($"Номер телефона: {consultant.GetClientNumber(clients[1])}");
            consultant.SetClientNumber(clients[1]);
            Console.WriteLine($"Номер телефона: {consultant.GetClientNumber(clients[1])}");

            consultant.PrintCLientPasportData(clients[0]);


            Console.ReadKey(true);
        }
    }
}
