using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    class Department
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
        /// департаменты в подчинении этого департамента
        /// </summary>
        private List<Department> departments;

        internal List<Department> Departments { get => departments; set => departments = value; }
        internal List<Person> Peson { get => _persons; set => _persons = value; }


    }
}
