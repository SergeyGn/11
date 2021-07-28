using Newtonsoft.Json;

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
        private int _countYearsPerson;
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public DateTime DateBirthDay { get => date; set => date = value; }
        public DateTime DateStartWork { get => _dateStartWork; set => _dateStartWork = value; }
        public int Experience { get => GetCountYears(DateStartWork); set => _experience = value; }
        public string NameDepartment { get => _nameDepartment; set => _nameDepartment = value; }
        public double Salary { get => GetSalary(); set => _salary = value; }
        public int CountYearsPerson { get=> GetCountYears(DateBirthDay); set => _countYearsPerson = value; }
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
           int dateEnd;
            if (date.Month < DateTime.Now.Month)
            {
                dateEnd = DateTime.Now.Year - date.Year;
            }
            else if (date.Month == DateTime.Now.Month)
            {
                if (date.Day <= DateTime.Now.Day)
                {
                    dateEnd = DateTime.Now.Year - date.Year;
                }
                else
                {
                    dateEnd = DateTime.Now.Year - date.Year - 1;
                }
            }
            else
            {
                dateEnd = DateTime.Now.Year - date.Year - 1;
            }
            return dateEnd;
        }


        abstract public double GetSalary();
    }
}
