using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_OOP2_WPF
{
    internal class Department
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public Department(string name, int departmentId)
        {
            Name = name;
            DepartmentId = departmentId;
        }

        public override string ToString()
        {
            return $"Департамент {DepartmentId}";
        }
    }
}
