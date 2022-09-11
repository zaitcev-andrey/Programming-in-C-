using System;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; // JObject, JArray
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4
{
    internal class Program
    {
        /// <summary>
        /// Производится сериализация данных пользователя(Фио, улица,
        /// номер дома, номер квартиры, мобильный телефон, домашний телефон)
        /// в формат xml
        /// </summary>
        /// <param name="path">Имя файла, куда запишутся данные</param>
        static void MyXmlSerialization(string path)
        {
            // Производим сериализацию в xml файл
            Console.WriteLine("Производим сериализацию в xml файл\n");

            // Создаём элементы, из которых будет состоять xml файл
            XElement person = new XElement("Person");
            XElement address = new XElement("Address");
            XElement phones = new XElement("Phones");
            XElement street = new XElement("Street");
            XElement houseNumber = new XElement("HouseNumber");
            XElement flatNumber = new XElement("FlatNumber");
            XElement mobilePhone = new XElement("MobilePhone");
            XElement flatPhone = new XElement("FlatPhone");

            Console.WriteLine("Введите ваши ФИО");
            XAttribute xAttributeFio = new XAttribute("name", Console.ReadLine());

            Console.WriteLine("Введите вашу улицу");
            XAttribute xAttributeStreet = new XAttribute("street_name", Console.ReadLine());
            street.Add(xAttributeStreet);

            Console.WriteLine("Введите ваш номер дома");
            XAttribute xAttributeHouseNumber = new XAttribute("house_number", Console.ReadLine());
            houseNumber.Add(xAttributeHouseNumber);

            Console.WriteLine("Введите ваш номер квартиры");
            XAttribute xAttributeFlatNumber = new XAttribute("flat_number", Console.ReadLine());
            flatNumber.Add(xAttributeFlatNumber);
            // После заполнения трёх элементов атрибутами добавляем все
            // три элемента в объединяющий их элемент - адрес
            address.Add(street, houseNumber, flatNumber);

            Console.WriteLine("Введите ваш мобильный телефон");
            XAttribute xAttributeMobilePhone = new XAttribute("mobile_phone_number", Console.ReadLine());
            mobilePhone.Add(xAttributeMobilePhone);

            Console.WriteLine("Введите ваш домашний телефон");
            XAttribute xAttributeFlatPhone = new XAttribute("flat_phone_number", Console.ReadLine());
            flatPhone.Add(xAttributeFlatPhone);
            phones.Add(mobilePhone, flatPhone);

            // В конце заполняем главный элемент xml файла.
            // Он состоит из 2 элементов и включает 1 атрибут
            person.Add(address, phones, xAttributeFio);

            // Через метод главного элемента сохраняем его в файл
            person.Save(path);
        }

        /// <summary>
        /// Производится сериализация данных пользователя(Фио, улица,
        /// номер дома, номер квартиры, мобильный телефон, домашний телефон)
        /// в формат json
        /// </summary>
        /// <param name="path">Имя файла, куда запишутся данные</param>
        static void MyJsonSerialization(string path)
        {
            // Производим сериализацию в json файл
            Console.WriteLine("Производим сериализацию в json файл\n");

            JObject jPerson = new JObject();
            JObject jAddress = new JObject();

            Console.WriteLine("Введите ваши ФИО");
            jPerson["name"] = Console.ReadLine();

            Console.WriteLine("Введите вашу улицу");
            jAddress["street_name"] = Console.ReadLine();
            Console.WriteLine("Введите ваш номер дома");
            jAddress["house_number"] = Console.ReadLine();
            Console.WriteLine("Введите ваш номер квартиры");
            jAddress["flat_number"] = Console.ReadLine();

            JObject jPhones = new JObject();
            Console.WriteLine("Введите ваш мобильный телефон");
            jPhones["mobile_phone_number"] = Console.ReadLine();
            Console.WriteLine("Введите ваш домашний телефон");
            jPhones["flat_phone_number"] = Console.ReadLine();
            
            jPerson["Address"] = jAddress;
            jPerson["Phones"] = jPhones;

            File.WriteAllText(path, jPerson.ToString());
        }

        static void Main(string[] args)
        {
            MyXmlSerialization("personData.xml");
            Console.WriteLine();
            MyJsonSerialization("personData.json");

            Console.ReadKey(true);
        }
    }
}
