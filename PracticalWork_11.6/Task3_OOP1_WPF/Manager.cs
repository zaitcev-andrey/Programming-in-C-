using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP1_WPF
{
    /// <summary>
    /// Класс менеджер будет иметь возможность просматривать и менять любые поля у клиента
    /// Поэтому реализуем все методы на изменение и получение полей, которых не хватает
    /// </summary>
    internal class Manager : Consultant, IManagerMethods
    {
        public Client AddNewNoteAboutClient()
        {
            Console.WriteLine("Давайте создадим новую запись о клиенте");
            Client client = new Client();
            SetClientFio(client);
            SetClientTelephoneNumber(client);
            SetClientPasportData(client);

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Добавлена новая запись о клиенте");

            return client;
        }

        public void AddTimeAndLogNoteAboutClient(Client client)
        {
            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Добавлена новая запись о клиенте");
        }

        public new void PrintCLientPasportData(Client client)
        {
            Console.WriteLine(client.Pasport);
        }
        public string GetClientPasportData(Client client)
        {
            return client.Pasport;
        }
        public void SetClientFio(Client client)
        {
            Console.WriteLine("Заполните ФИО клиента");
            Console.Write("Введите фамилию: ");
            client.SecondName = Console.ReadLine();
            Console.Write("Введите имя: ");
            client.FirstName = Console.ReadLine();
            Console.Write("Введите отчество: ");
            client.MiddleName = Console.ReadLine();

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменены ФИО клиента");
        }

        public void SetClientFirstName(Client client)
        {
            Console.Write("Введите имя клиента: ");
            client.FirstName = Console.ReadLine();

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменено имя клиента");
        }

        public void SetClientLastName(Client client)
        {
            Console.Write("Введите фамилию клиента: ");
            client.SecondName = Console.ReadLine();

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменена фамилия клиента");
        }

        public void SetClientMiddleName(Client client)
        {
            Console.Write("Введите отчество клиента: ");
            client.MiddleName = Console.ReadLine();

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменено отчество клиента");
        }

        public void SetClientPasportData(Client client)
        {
            StringBuilder result = new StringBuilder();
            bool flag = true;
            while (flag)
            {
                Console.Write("Введите серию паспорта (это 4 цифры): ");
                string series = Console.ReadLine();
                if (series.Length == 4)
                {
                    foreach (char c in series)
                    {
                        if (c < '0' || c > '9')
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        flag = false;
                        result.Append(series);
                    }
                    else
                    {
                        Console.WriteLine("Вы ошиблись при вводе серии, попробуйте снова");
                        flag = true;
                    }
                }
                else
                    Console.WriteLine("Вы ошиблись при вводе серии, попробуйте снова");
            }
            flag = true;
            result.Append(" ");
            while (flag)
            {
                Console.Write("Введите номер паспорта (это 6 цифр): ");
                string number = Console.ReadLine();
                if (number.Length == 6)
                {
                    foreach (char c in number)
                    {
                        if (c < '0' || c > '9')
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        flag = false;
                        result.Append(number);
                    }
                    else
                    {
                        Console.WriteLine("Вы ошиблись при вводе номера, попробуйте снова");
                        flag = true;
                    }
                }
                else
                    Console.WriteLine("Вы ошиблись при вводе номера, попробуйте снова");
            }
            client.Pasport = result.ToString();

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменены паспортные данные клиента");

        }

        /// <summary>
        /// Метод для WPF по изменению паспортных данных клиента
        /// </summary>
        /// <param name="client"></param>
        /// <param name="pasportSeries"></param>
        /// <param name="pasportNumber"></param>
        /// <returns></returns>

        public bool CheckPasportSeries(string pasportSeries)
        {
            if (pasportSeries.Length == 4)
            {
                foreach (char c in pasportSeries)
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool CheckPasportNumber(string pasportNumber)
        {
            if (pasportNumber.Length == 6)
            {
                foreach (char c in pasportNumber)
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public bool SetClientPasportData(Client client, string pasportSeries, string pasportNumber)
        {
            StringBuilder result = new StringBuilder();
            if (CheckPasportSeries(pasportSeries))
            {
                if(CheckPasportNumber(pasportNumber))
                {
                    result.Append($"{pasportSeries} ");
                    result.Append(pasportNumber);
                    client.Pasport = result.ToString();

                    client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                        "Изменены паспортные данные клиента");
                    return true;
                }
            }
            return false;
        }

        public void SetClientFio(Client client, string lastName, string firstName, string middleName)
        {
            client.SecondName = lastName;
            client.FirstName = firstName;
            client.MiddleName = middleName;

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name,
                "Изменены ФИО клиента");
        }
    }
}
