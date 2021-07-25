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
        public static string NameWithoutDepartment = "(без департамента)";
        public static Department MainDepartment = new Department("mainDepartment");
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
                Department WithoutDepartment = new Department(NameWithoutDepartment);
                WithoutDepartment.IsMainDepartment = true;
                MainDepartment.Departments.Add(WithoutDepartment);

            }
            InitializeComponent();
            ListDepartments.ItemsSource = MainDepartment.Departments;
            
             
        }

        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            CurrentDepartment = MainDepartment.Departments[0];
            CDepartmentWindow CreateDepartmentWindow = new CDepartmentWindow(MainDepartment.Departments[0], MainDepartment.Departments[0].Persons); //MainDepartment.Departments[0].Persons - это сотрудники без деп.
            
            CreateDepartmentWindow.Owner = this;
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
                DeletePersonWindow.InfoPanel.Text = "Удалить сотрудника?";
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
            if ( CurrentDepartment == null)
            {
              MessageBoxResult messageBox = MessageBox.Show("Выберите департамент для редактирования");
            }
            else if(CurrentDepartment == MainDepartment.Departments[0])
            {
                MessageBoxResult messageBox = MessageBox.Show("Выберите департамент для редактирования");
            }
            else
            {
                CDepartmentWindow CreateDepartmentWindow = new CDepartmentWindow(CurrentDepartment, CurrentDepartment.Persons);
                CreateDepartmentWindow.Owner = this;
                CreateDepartmentWindow.Title = "Edit Department";
                CreateDepartmentWindow.NameDepartment.Text = CurrentDepartment.DepartmentName;

                CreateDepartmentWindow.ShowDialog();
                RefreshMainWindow();
            }
        }

        private void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentDepartment == null)
            {
                MessageBoxResult messageBox = MessageBox.Show("Выберите департамент для удаления");
            }
            else if (CurrentDepartment == MainDepartment.Departments[0])
            {
                MessageBoxResult messageBox = MessageBox.Show("Выберите департамент для удаления");
            }
            else
            {
                for (int i = 0; i < MainWindow.MainDepartment.Departments.Count; i++)
                {

                    if (MainWindow.MainDepartment.Departments[i].Departments.Count > 0)
                    {
                        for (int j = 0; j < MainWindow.MainDepartment.Departments[i].Departments.Count; j++)
                        {
                            if (MainDepartment.Departments[i].Departments[j].MainDepartmentName == CurrentDepartment.DepartmentName)
                            {
                                //все департаменты у которых главный был удален, носят статус "без деп" т.е. свободных
                                MainDepartment.Departments[i].Departments[j].MainDepartmentName = NameWithoutDepartment;
                                MainDepartment.Departments[i].Departments[j].IsMainDepartment = false;
                            }

                            if (MainDepartment.Departments[i].Departments[j] == CurrentDepartment)
                            {
                                MainDepartment.Departments[i].Departments.Remove(MainDepartment.Departments[i].Departments[j]);

                            }
                        }
                    }
                    if (MainDepartment.Departments[i] == CurrentDepartment)
                    {
                        MainDepartment.Departments.Remove(MainDepartment.Departments[i]);
                    }
                    if (MainDepartment.Departments[i].MainDepartmentName == CurrentDepartment.DepartmentName)
                    {
                        //все департаменты у которых главный был удален, носят статус "без деп" т.е. свободных
                        MainDepartment.Departments[i].MainDepartmentName = NameWithoutDepartment;
                        MainDepartment.Departments[i].IsMainDepartment = false;
                    }
                }

                //увольнение сотрудников в запас т.е. отправление в депратамент "без деп"
                for (int i = 0; i < CurrentDepartment.Persons.Count; i++)
                {
                    CurrentDepartment.Persons[i].NameDepartment = MainDepartment.Departments[0].DepartmentName;
                    MainDepartment.Departments[0].Persons.Add(CurrentDepartment.Persons[i]);
                    MainDepartment.Departments[0].CountPerson++;
                }
                RefreshMainWindow();
            }
        }

        private void ListPersons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetInfoPanelWindow();
        }

        private void PersonsInDepartment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GetInfoPanelWindow();
        }
        private void GetInfoPanelWindow()
        {
            InfoPersonWindow ShowPersonWindow = new InfoPersonWindow();
            ShowPersonWindow.Owner = this;
            ShowPersonWindow.Title = "Show Person";
            ShowPersonWindow.ButtonCreatePersonCencel.Content = "OK";
            ShowPersonWindow.ButtonCreatePersonOk.Visibility = Visibility.Hidden;

            ShowPersonWindow.ShowDialog();
            RefreshMainWindow();
        }
    }
}
