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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _11_InfoAboutDepartment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Department MainDepartment = new Department("mainDepartment");
        public static Department WithoutDepartment;
        public static Person CurrentPerson;
        public static Department CurrentDepartment;

        public MainWindow()
        {

            if (File.Exists(Program.path))
            {
                MainDepartment = Program.Load();
            }
            else
            {
                WithoutDepartment = new Department("(без департамента)");
                MainDepartment.Departments.Add(WithoutDepartment);
            }
            InitializeComponent();
            ListDepartments.ItemsSource = MainDepartment.Departments;
            
             
        }

        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            CDepartmentWindow CreateDepartmentWindow = new CDepartmentWindow();
            CreateDepartmentWindow.Owner = this;
            CreateDepartmentWindow.department = MainDepartment;
            CreateDepartmentWindow.personsInCurrentDepartment = MainDepartment.Departments[0].Persons;
            CreateDepartmentWindow.ShowDialog();

            RefreshMainWindow();
        }
        
        private void CreatePerson_Click(object sender, RoutedEventArgs e)
        {
            CPersonWindow CreatePersonWindow = new CPersonWindow();
            CreatePersonWindow.Owner = this;
            CreatePersonWindow.ShowDialog();

            RefreshMainWindow();
        }


        private void ListDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Department currentDep = new Department();
           currentDep = ListDepartments.SelectedItem as Department;
            if(currentDep==null)
            {
                currentDep = MainDepartment.Departments[0];
            }
           ListPersons.ItemsSource = currentDep.Persons;
           DepartmentsInDepartment.ItemsSource = currentDep.Departments;

            CurrentDepartment = currentDep;
        }

        private void DepartmentsInDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department currentDep = new Department();
            currentDep = DepartmentsInDepartment.SelectedItem as Department;
            PersonsInDepartment.ItemsSource = currentDep.Persons;

            CurrentDepartment = currentDep;
        }

        private void PersonsInDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPerson = PersonsInDepartment.SelectedItem as Person;
        }

        private void RefreshMainWindow()
        {
            ListDepartments.Items.Refresh();
            DepartmentsInDepartment.Items.Refresh();
            ListPersons.Items.Refresh();
            PersonsInDepartment.Items.Refresh();
        }

        private void ListPersons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPerson = ListPersons.SelectedItem as Person;
        }

        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPerson != null)
            {
                CPersonWindow EditPersonWindow = new CPersonWindow();
                EditPersonWindow.Owner = this;
                EditPersonWindow.Title = "Edit Person";

                EditPersonWindow.FirstName.Text = CurrentPerson.FirstName;
                EditPersonWindow.LastName.Text = CurrentPerson.LastName;
                EditPersonWindow.DateBirth.Text = CurrentPerson.DateBirthDay.ToShortDateString();
                EditPersonWindow.DateEmployment.Text = CurrentPerson.DateStartWork.ToShortDateString();
                EditPersonWindow.NameDepartment.Text = CurrentPerson.NameDepartment;
                EditPersonWindow.Profession.Text = CurrentPerson.Profession;

                if (CurrentPerson.Profession == "рабочий")
                {
                    EditPersonWindow.CountDay.Value = (CurrentPerson as Employee).CountWorkingDay;
                }

                EditPersonWindow.ShowDialog();
                RefreshMainWindow();
            }
            else
            {
                MessageBoxResult messageBox = MessageBox.Show("Выберите человека для редактирования");
            }
        }

        public static void DeleteCurrentPerson()
        {
            for (int i = 0; i < MainDepartment.Departments.Count; i++)
            {
                for (int j = 0; j < MainDepartment.Departments[i].Persons.Count; j++)
                {
                    if (CurrentPerson == MainDepartment.Departments[i].Persons[j])
                    {
                        MainDepartment.Departments[i].Persons.Remove(MainDepartment.Departments[i].Persons[j]);
                        MainDepartment.Departments[i].CountPerson--;
                    }
                }
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPerson != null)
            {
                InfoPersonWindow DeletePersonWindow = new InfoPersonWindow();
                DeletePersonWindow.Owner = this;
                DeletePersonWindow.Title = "Delete Person";

                DeletePersonWindow.FirstName.Text = CurrentPerson.FirstName;
                DeletePersonWindow.LastName.Text = CurrentPerson.LastName;
                DeletePersonWindow.DateBirth.Text = CurrentPerson.DateBirthDay.ToShortDateString();
                DeletePersonWindow.DateEmployment.Text = CurrentPerson.DateStartWork.ToShortDateString();
                DeletePersonWindow.NameDepartment.Text = CurrentPerson.NameDepartment;
                DeletePersonWindow.Profession.Text = CurrentPerson.Profession;
                DeletePersonWindow.Salary.Text = CurrentPerson.Salary.ToString();

                DeletePersonWindow.ShowDialog();
                RefreshMainWindow();
            }
            else
            {
                MessageBoxResult messageBox = MessageBox.Show("Выберите человека для удаления");
            }
        }

        private void EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            CDepartmentWindow CreateDepartmentWindow = new CDepartmentWindow();
            CreateDepartmentWindow.Owner = this;
            CreateDepartmentWindow.Title = "Edit Department";
            CreateDepartmentWindow.department = CurrentDepartment;
            CreateDepartmentWindow.personsInCurrentDepartment = CurrentDepartment.Persons;
            CreateDepartmentWindow.ListDepartments.SelectAll();
            CreateDepartmentWindow.PersonInDepartment.SelectAll();


            CreateDepartmentWindow.ShowDialog();

            RefreshMainWindow();
        }
    }
}
