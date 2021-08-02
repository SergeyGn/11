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
        private int _experiance;
        private double _salary;
        private int _countYearsPerson;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirthDay { get; set; }
        public DateTime DateStartWork { get; set; }
        public int Experience { get => GetCountYears(DateStartWork); set => _experiance = value; }
        public string NameDepartment { get; set; }
        public double Salary { get => GetSalary(); set => _salary = value; }
        public int CountYearsPerson { get=> GetCountYears(DateBirthDay); set => _countYearsPerson = value; }
        public string Profession { get; set; }

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
