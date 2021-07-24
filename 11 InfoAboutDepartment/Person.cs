using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
  public abstract class Person
    {
        private string _firstName;
        private string _lastName;
        private DateTime date;
        private DateTime _dateStartWork;
        private int _experience;
        private string _nameDepartment;
        private double _salary;
        private string _profession;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime DateBirthDay { get => date; set => date = value; }
        public DateTime DateStartWork { get => _dateStartWork; set => _dateStartWork = value; }
        public int Experience { get => _experience; set => _experience = value; }
        public string NameDepartment { get => _nameDepartment; set => _nameDepartment = value; }
        public double Salary { get => _salary; set => _salary = value; }
        public int CountYearsPerson { get; set; }
        public string Profession { get => _profession; set => _profession = value; }

        public Person(string FirstName,string LastName,DateTime DateBirthDay,DateTime DateStartWork,string NameDepartment, string Profession)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateBirthDay = DateBirthDay;
            this.DateStartWork = DateStartWork;
            this.NameDepartment = NameDepartment;
            this.Profession = Profession;


            CountYearsPerson = GetCountYears(DateBirthDay);
            Experience = GetCountYears(DateStartWork);
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
