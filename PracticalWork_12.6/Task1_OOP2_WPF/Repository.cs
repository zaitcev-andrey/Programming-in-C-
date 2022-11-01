using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Task1_OOP2_WPF
{
    internal class Repository
    {
        public List<Department> Departments { get; set; }
        public List<Client> Clients { get; set; }

        private int[] saveIdInDepartments;
        public Repository(int countDepartments = 3)
        {
            Departments = new List<Department>();
            Clients = new List<Client>();
            saveIdInDepartments = new int[countDepartments];

            for (int i = 0; i < countDepartments; i++)
            {
                Departments.Add(new Department("Департамент", i + 1));
            }

            Random rand = new Random();

            int randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Иван", "Иванов", "Иванович", 34, "89998762315", "7718 999888", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Сергей", "Сергеев", "Сергеевич", 19, "87778762315", "3466 999888", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Пётр", "Петров", "Петрович", 24, "85558762315", "1922 999888", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Александра", "Сидорова", "Артёмовна", 44, "83338762315", "9813 999888", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Евгений", "Прокофьев", "Максимович", 43, "87285620267", "7751 593018", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Михаил", "Смирнов", "Артёмович", 32, "88076452003", "9843 864390", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Андрей", "Большунов", "Антонович", 51, "83658605209", "1345 820441", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Никита", "Гришин", "Андреевич", 22, "81357845724", "7842 122941", randomDepId, GetIdInDepartment(randomDepId)));
            randomDepId = rand.Next(1, countDepartments + 1);
            Clients.Add(new Client("Иван", "Кузьмин", "Генадьевич", 60, "87236505713", "5632 863728", randomDepId, GetIdInDepartment(randomDepId)));
        }

        private int GetIdInDepartment(int departmentId)
        {
            return (++saveIdInDepartments[departmentId - 1]);
        }

        public int GetMaxIdFromDepartmentsArray(int departmentId)
        {
            return saveIdInDepartments[departmentId - 1];
        }

        // Нужен для менеджера при добавлении записи
        public void IncreaseIdInDepartment(int departmentId)
        {
            ++saveIdInDepartments[departmentId - 1];
        }
    }
}
