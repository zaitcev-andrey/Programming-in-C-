using System;
using System.Collections.Generic;
using System.Text;

namespace Task3_OOP1_WPF
{
    internal class Client
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Pasport { get; set; }

        public DateTime dateTime { get; set; }
        public string WhoChangedData { get; set; }
        public string WhatDataIsChange { get; set; }
        private string LogAboutChanges; // скроем лог информации, чтобы в файле json он не дублировался

        public Client(string firstName, string secondName,
            string middleName, string telephoneNumber,
            string pasport, string whoChangedData = "",
            string whatDataIsChange = "")
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
            TelephoneNumber = telephoneNumber;
            Pasport = pasport;
            dateTime = DateTime.Now;
            WhoChangedData = whoChangedData;
            WhatDataIsChange = whatDataIsChange;
        }

        // конструктор по умолчанию
        public Client() : this("", "", "", "", "") { }

        public void SaveChanges(string dateTime, string WhoChangedData, string WhatDataIsChange)
        {
            this.dateTime = DateTime.Parse(dateTime);
            this.WhoChangedData = WhoChangedData;
            this.WhatDataIsChange = WhatDataIsChange;

            StringBuilder sb = new StringBuilder();
            sb.Append($"Время изменения записи: {dateTime}");
            sb.Append($"\nКакие данные изменены: {WhatDataIsChange}");
            sb.Append($"\nКто изменил данные: {WhoChangedData}");
            LogAboutChanges = sb.ToString();
        }

        public void CheckChanges()
        {
            if (string.IsNullOrEmpty(LogAboutChanges))
                Console.WriteLine("Изменений пока что нет");
            else
                Console.WriteLine("Последние изменения:\n" + LogAboutChanges);
        }

        public void PrintFio()
        {
            Console.WriteLine($"ФИО клиента: {SecondName} {FirstName} {MiddleName}");
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
