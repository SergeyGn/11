﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    class Employee:Person
    {
        public Employee(string FirstName, string LastName, DateTime DateBirthDay, DateTime DateStartWork, string NameDepartment, int CountWorkingDay)
            : base(FirstName, LastName, DateBirthDay, DateStartWork, NameDepartment,"Рабочий")
        {
            this.CountWorkingDay = CountWorkingDay;
            Salary = GetSalary();
        }

        static double _wokingHouse=10; // почасовая оплата
        const int _countWorkingHoursInDay=8; //Кол-во рабочих часов в день

        public int CountWorkingDay { get; set; }

        public override double GetSalary()
        {
            double salary = CountWorkingDay * _countWorkingHoursInDay * _wokingHouse;
            return salary;
        }
    }
}
