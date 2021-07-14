using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    class Intern:Person
    {
        const double _fixedSalary=300;

        public Intern(string FirstName, string LastName, DateTime DateBirthDay, DateTime DateStartWork, string NameDepartment) 
            : base(FirstName, LastName, DateBirthDay, DateStartWork, NameDepartment,"Интерн")
        {
            Salary = GetSalary();
        }

        public override double GetSalary()
        {
            return _fixedSalary;
        }
    }
}
