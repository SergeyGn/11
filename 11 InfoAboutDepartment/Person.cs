using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
   abstract class Person
    {
        private string _firstName;
        private string _lastName;
        private DateTime date;
        private DateTime _dateStartWork;
        private int _experience;
        private string _nameDepartment;
        private double _salary;

        protected string FirstName { get => _firstName; set => _firstName = value; }
        protected string LastName { get => _lastName; set => _lastName = value; }
        protected DateTime DateBirthDay { get => date; set => date = value; }
        protected DateTime DateStartWork { get => _dateStartWork; set => _dateStartWork = value; }
        protected int Experience { get => _experience; set => _experience = value; }
        protected string NameDepartment { get => _nameDepartment; set => _nameDepartment = value; }
        protected double Salary { get => _salary; set => _salary = value; }
        protected int CountYearsPerson { get; set; }

        public Person(string FirstName,string LastName,DateTime DateBirthDay,DateTime DateStartWork,string NameDepartment
             )
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateBirthDay = DateBirthDay;
            this.DateStartWork = DateStartWork;
            this.NameDepartment = NameDepartment;


            CountYearsPerson = GetCountYears(DateBirthDay);
            Experience = GetCountYears(DateStartWork);
            Salary = GetSalary();
        }

        public int GetCountYears(DateTime date)
        {
            if (this.date.Month < DateTime.Now.Month)
            {
                CountYearsPerson = DateTime.Now.Year - this.date.Year;
            }
            else if (this.date.Month == DateTime.Now.Month)
            {
                if (this.date.Day <= DateTime.Now.Day)
                {
                    CountYearsPerson = DateTime.Now.Year - this.date.Year;
                }
                else
                {
                    CountYearsPerson = DateTime.Now.Year - this.date.Year - 1;
                }
            }
            else
            {
                CountYearsPerson = DateTime.Now.Year - this.date.Year - 1;
            }
            return CountYearsPerson;
        }


        abstract public double GetSalary();
    }
}
