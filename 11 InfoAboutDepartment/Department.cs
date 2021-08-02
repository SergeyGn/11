using System.Collections.Generic;


namespace _11_InfoAboutDepartment
{
    public class Department
    {
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

        private int _countPerson;
        private int _countDepartment;
        public string DepartmentName { get; set; }
        public List<Department> Departments { get; set; }
        public List<Person> Persons { get; set; }
        public int CountDepartment { get => GetCountDepartment(); set => _countDepartment = value; }
        public int CountPerson { get => GetCountPerson(); set => _countPerson = value; }
        public bool IsMainDepartment { get; set; }
        public string MainDepartmentName { get; set; }
    }
}
