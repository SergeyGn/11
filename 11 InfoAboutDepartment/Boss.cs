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
        private const double _minDilaryBoss = 1300; //минимальная зп по тз 
        Department _department;
        public Boss(string FirstName, string LastName, DateTime DateBirthDay, DateTime DateStartWork, string NameDepartment, Department department) 
            : base(FirstName, LastName, DateBirthDay, DateStartWork, NameDepartment)
        {
            Salary = GetSalary();
        }

        internal Department Department { get => _department; set => _department = value; }

        public override double GetSalary()
        {
            double dilaryAllWorkersDepartment=0;
            for(int i=0; i<Department.Peson.Count;i++) //перечисляем всех сотрудников в департаменте начальника
            {
                dilaryAllWorkersDepartment += Department.Peson[i].GetSalary();
            }
            for (int i = 0; i < Department.Departments.Count; i++)
            {
                if (Department.Departments[i].Peson.Count <= 0)
                {
                    for (int j = 0; j < Department.Departments[i].Peson.Count; j++)
                    {
                        dilaryAllWorkersDepartment += Department.Departments[i].Peson[j].GetSalary();
                    }
                }
            }
            if (dilaryAllWorkersDepartment*_percent<=_minDilaryBoss)
            {
                return _minDilaryBoss;
            }
            else
            {
                return dilaryAllWorkersDepartment * _percent;
            }
        }
    }
}
