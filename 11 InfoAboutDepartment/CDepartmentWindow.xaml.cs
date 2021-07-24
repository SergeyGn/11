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
        ObservableCollection<Department> departmentsList = new ObservableCollection<Department>();
        ObservableCollection<Person> personsList = new ObservableCollection<Person>();


        public CDepartmentWindow(Department department, List<Person> persons)
        {

            InitializeComponent();



            for (int i = 0; i < department.Departments.Count; i++) 
            {
                if (department.Departments[i].DepartmentName != MainWindow.NameWithoutDepartment) //чтобы не показывать строчку "без департаментов"
                {
                    departmentsList.Add(department.Departments[i]);
                }
            }



            for (int i = 0; i < persons.Count; i++) 
            {
                personsList.Add(persons[i]);
            }
            


            
            ListDepartments.ItemsSource = departmentsList;
            PersonInDepartment.ItemsSource = personsList;
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
                    for (int j = 0; j < personsList.Count; j++)
                    {
                        if (PersonInDepartment.SelectedItems[i] == personsList[j])
                            AddPerson.Add(personsList[j]);
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
                        if (MainWindow.MainDepartment.Departments[i] == MainWindow.CurrentDepartment)
                        {
                            MainWindow.MainDepartment.Departments[i] = newDepartment;
                        }

                        for (int j=0;j< MainWindow.MainDepartment.Departments[i].Departments.Count; j++)
                        {

                            if (MainWindow.MainDepartment.Departments[i].Departments[j] == MainWindow.CurrentDepartment)
                            {
                                MainWindow.MainDepartment.Departments[i].Departments[j] = newDepartment;

                            }
                        }
                    }

                    //увольнение сотрудников в запас
                    for (int i = 0; i < PersonInDepartment.Items.Count; i++)
                    {
                        if(PersonInDepartment.Items[i]!=PersonInDepartment.SelectedItem)
                        {
                            MainWindow.MainDepartment.Departments[0].Persons.Add(PersonInDepartment.Items[i] as Person);
                            MainWindow.MainDepartment.Departments[0].CountPerson++;
                        }

                    }
                }
                else
                {
                    MainWindow.MainDepartment.Departments.Add(newDepartment);
                }
                Program.Save(MainWindow.MainDepartment);
                Close();
            }

        }

        private void ButtonCreateDepartmentCencel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
