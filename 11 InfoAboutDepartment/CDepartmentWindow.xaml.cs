using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace _11_InfoAboutDepartment
{
    /// <summary>
    /// Логика взаимодействия для CDepartmentWindow.xaml
    /// </summary>
    public partial class CDepartmentWindow : Window
    {
        Department newDepartment;
        ObservableCollection<Department> departments = new ObservableCollection<Department>();
        ObservableCollection<Person> persons = new ObservableCollection<Person>();
        public  Department department;
        public List<Person> personsInCurrentDepartment;

        public CDepartmentWindow()
        {
            department = new Department();
            personsInCurrentDepartment = new List<Person>();
            InitializeComponent();



            for (int i = 0; i < department.Departments.Count; i++) 
            {
                departments.Add(department.Departments[i]);
            }



            for (int i = 0; i < personsInCurrentDepartment.Count; i++) 
            {
                persons.Add(department.Persons[i]);
            }
            


            
            ListDepartments.ItemsSource = departments;
            PersonInDepartment.ItemsSource = persons;
        } 

        private void ButtonCreateDepartmentOk_Click(object sender, RoutedEventArgs e)
        {
            if (NameDepartment.Text == "")
            {
                TextInfo.Text = "Заполните поле с именем департамента";
            }
            else
            {
                string nameDepartment = NameDepartment.Text;

                List<Department> AddDepartment = new List<Department>();

                for (int i = 0; i < ListDepartments.SelectedItems.Count; i++)
                {
                    AddDepartment.Add(ListDepartments.SelectedItems[i] as Department);
                }

                List<Person> AddPerson = new List<Person>();

                for (int i = 0; i < PersonInDepartment.SelectedItems.Count; i++)
                {
                    for (int j = 0; j < persons.Count; j++)
                    {
                        if (PersonInDepartment.SelectedItems[i] == persons[j])
                            AddPerson.Add(persons[j]);
                    }
                }


                newDepartment = new Department(nameDepartment);
                newDepartment.Departments = AddDepartment;
                newDepartment.Persons = AddPerson;
                newDepartment.CountDepartment = AddDepartment.Count;



                if(CreateDepartmentWindow.Title == "Edit Department")
                {
                    for(int i=0;i<MainWindow.MainDepartment.Departments.Count;i++)
                    {
                        
                        for(int j=0;j< MainWindow.MainDepartment.Departments[i].Departments.Count; j++)
                        {
                            MainWindow.MainDepartment.Departments[i] = newDepartment;
                            if (MainWindow.MainDepartment.Departments[i].Departments[j]==MainWindow.CurrentDepartment)
                            {
                                MainWindow.MainDepartment.Departments[i].Departments[j]=newDepartment;
                                
                            }
                        }
                    }
                }
                else
                {
                    MainWindow.MainDepartment.Departments.Add(newDepartment);
                }
                //Program.Save(MainWindow.MainDepartment);
                Close();
            }

        }

        private void ButtonCreateDepartmentCencel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
