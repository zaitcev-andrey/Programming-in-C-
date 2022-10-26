using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_OOP1
{
    internal class Client
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Pasport { get; set; }

        public Client(string firstName, string secondName,
            string middleName, string telephoneNumber,
            string pasport)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
            TelephoneNumber = telephoneNumber;
            Pasport = pasport;
        }

        public string GetInfoForConsultantAboutPasport()
        {
            if (string.IsNullOrEmpty(Pasport))
                return "Данных о паспорте нет!";
            else
                return $"Серия и номер паспорта у клиента {SecondName} {FirstName} {MiddleName} скрыты: **** ******";
        }
    }
}
