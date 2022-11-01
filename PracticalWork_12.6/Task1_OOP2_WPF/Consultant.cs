﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP2_WPF
{
    /// <summary>
    /// Класс консультанта сможет просматривать поля кроме паспорта, 
    /// а также сможет менять номер телефона у клиента - это метод из интерфейса
    /// </summary>
    internal class Consultant : IConsultantMethods
    {
        public void PrintCLientPasportData(Client client)
        {
            Console.WriteLine(client.GetInfoForConsultantAboutPasport());
        }

        public void PrintClientFIO(Client client)
        {
            Console.WriteLine($"ФИО клиента: {client.LastName} {client.FirstName} {client.MiddleName}");
        }
        private string GetClientFIO(Client client)
        {
            return $"{client.LastName} {client.FirstName} {client.MiddleName}";
        }
        /// <summary>
        /// Изменение номера телефона клиента
        /// </summary>
        /// <param name="client"></param>
        public void SetClientTelephoneNumber(Client client)
        {
            string new_number = "";
            bool flag = true;
            while (flag)
            {
                Console.Write($"Введите номер телефона клиента {GetClientFIO(client)}: ");
                new_number = Console.ReadLine();
                if (!string.IsNullOrEmpty(new_number))
                {
                    foreach (char c in new_number)
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
                    }
                    else
                    {
                        Console.WriteLine("Вы ошиблись при вводе номера, попробуйте снова");
                        flag = true;
                    }
                }
            }
            client.TelephoneNumber = new_number;

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name, "Изменён номер телефона");
        }

        public void SetClientTelephoneNumber(Client client, string telephoneNumber)
        {
            client.TelephoneNumber = telephoneNumber;

            client.SaveChanges(DateTime.Now.ToString(), GetType().Name, "Изменён номер телефона");
        }

        public bool CheckClientTelephoneNumber(string telephoneNumber)
        {
            if (!string.IsNullOrEmpty(telephoneNumber))
            {
                foreach (char c in telephoneNumber)
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
        public string GetClientNumber(Client client)
        {
            return client.TelephoneNumber;
        }
    }
}
