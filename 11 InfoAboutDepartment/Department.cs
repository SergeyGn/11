using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    public class Department
    {

        /// <summary>
        /// Название департамента
        /// </summary>
        private string _departmentName;
        /// <summary>
        /// сотрудники в подчинении департамента
        /// </summary>
        private List<Person> _persons;
        /// <summary>
        /// кол-во департаментов в подчинении
        /// </summary>
        private int _countDepartment;
        /// <summary>
        /// кол-во сотрудников в департаменте
        /// </summary>
        private int _countPerson;

        public Department(string DepartmentName)
        {
            Persons = new List<Person>();
            Departments = new List<Department>();
            this.DepartmentName = DepartmentName;

            CountPerson = GetCountPerson();
            CountDepartment = GetCountDepartment();
        }

        public Department(string DepartmentName, List<Person> Persons, List<Department> Departments)
        {
            this.DepartmentName = DepartmentName;
            this.Persons = Persons;
            this.Departments = Departments;
            CountPerson = GetCountPerson();
            CountDepartment = GetCountDepartment();
        }



        public Department():this("",new List<Person>(),new List<Department>())
        {
        }

        public int GetCountPerson()
        {
            int CountPer = Persons.Count;
            return CountPer;
        }

        public int GetCountDepartment()
        {
            int CountDep = Departments.Count;
            return CountDep;
        }

        public string DepartmentName { get => _departmentName; set => _departmentName = value; }
        public List<Department> Departments { get; set; }
        public List<Person> Persons { get => _persons; set => _persons = value; }
        public int CountDepartment { get => _countDepartment; set => _countDepartment = value; }
        public int CountPerson { get => _countPerson; set => _countPerson = value; }
    }
}
