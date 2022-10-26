using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP1
{
    internal class Client
    {
        public string FirstName { get; }
        public string SecondName { get; }
        public string MiddleName { get; }
        public string TelephoneNumber { get; set; }
        public string Pasport { get; }

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
