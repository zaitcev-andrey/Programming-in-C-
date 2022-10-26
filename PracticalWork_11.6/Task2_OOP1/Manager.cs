using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_OOP1
{
    /// <summary>
    /// Класс менеджер будет иметь возможность просматривать и менять любые поля у клиента
    /// Поэтому реализуем все методы на изменение и получение полей, которых не хватает
    /// </summary>
    internal class Manager : Consultant
    {
        public new void PrintCLientPasportData(Client client)
        {
            Console.WriteLine(client.Pasport);
        }
        public void SetClientFio(Client client)
        {
            Console.WriteLine("Введём ФИО клиента");
            Console.Write("Введите фамилию: ");
            client.SecondName = Console.ReadLine();
            Console.Write("Введите имя: ");
            client.FirstName = Console.ReadLine();
            Console.Write("Введите отчество: ");
            client.MiddleName = Console.ReadLine();
        }
        public void SetClientPasportData(Client client)
        {
            StringBuilder result = new StringBuilder();
            bool flag = true;
            while(flag)
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

        }

    }
}
