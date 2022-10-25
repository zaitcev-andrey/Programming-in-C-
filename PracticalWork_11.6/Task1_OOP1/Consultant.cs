using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP1
{
    internal class Consultant
    {
        public void PrintCLientPasportData(Client client)
        {
            Console.WriteLine(client.GetInfoForConsultantAboutPasport());
        }

        public void PrintClientFIO(Client client)
        {
            Console.WriteLine($"ФИО клиента: {client.SecondName} {client.FirtstName} {client.MiddleName}");
        }
        private string GetClientFIO(Client client)
        {
            return $"{client.SecondName} {client.FirtstName} {client.MiddleName}";
        }
        /// <summary>
        /// Изменение номера телефона клиента
        /// </summary>
        /// <param name="client"></param>
        public void SetClientNumber(Client client)
        {
            string new_number = "";
            bool flag = true;
            while(flag)
            {
                Console.Write($"Введите новый номер телефона у клиента {GetClientFIO(client)}: ");
                new_number = Console.ReadLine();
                if(!string.IsNullOrEmpty(new_number))
                {
                    foreach(char c in new_number)
                    {
                        if(c < '0' || c > '9')
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
                        flag = true;
                }
            }
            client.TelephoneNumber = new_number;
        }

        public string GetClientNumber(Client client)
        {
            return client.TelephoneNumber;
        }
    }
}
