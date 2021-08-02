using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    class Boss : Person
    {
        private const double _percent = 0.15; //15 процентов по тз
        private const double _minSalaryBoss = 1300; //минимальная зп по тз 

        public Boss() : this("", "", DateTime.Now, DateTime.Now, "", new Department())
        {
        }

        public Boss(string FirstName, string LastName, DateTime DateBirthDay, DateTime DateStartWork, string NameDepartment, 
            Department Department) 
            : base(FirstName, LastName, DateBirthDay, DateStartWork, NameDepartment, $"Начальник {NameDepartment}")
        {
            this.Department = Department;
            Salary = GetSalary();
        }

        public Department Department { get; set; }

        public override double GetSalary()
        {
                double SalaryAllWorkersDepartment = GetAllSalary(Department);
            if (SalaryAllWorkersDepartment * _percent <= _minSalaryBoss)
            {
                    return _minSalaryBoss;
            }
            else
            {
                return SalaryAllWorkersDepartment * _percent;
            }

        }

        private double GetAllSalary(Department dep)
        {
            double AllSalary = 0;
            if (dep.Persons.Count > 0)
            {
                for (int i = 0; i < dep.Persons.Count; i++)
                {
                    if (dep.Persons[i] != this)
                    {
                        AllSalary += dep.Persons[i].Salary;
                    }
                }
            }
            if (dep.Departments.Count > 0)
            {
                for (int i = 0; i < dep.Departments.Count; i++)
                {
                    AllSalary += GetAllSalary(dep.Departments[i]);
                }
            }
            return AllSalary;
        }
    }
}
