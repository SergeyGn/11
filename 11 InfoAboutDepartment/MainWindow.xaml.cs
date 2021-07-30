using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private GridViewColumnHeader listViewSortCol = null;
        private string _currentTag;
        public static string Path;

        public MainWindow()
        {
            InitializeComponent();
            
            if (StartWindow.IsTest==true)
            {
                Path = Program.TestFilePath;
            }
            else
            {
                Path = Program.FilePath;
            }

            if (File.Exists(Path))
            {
                MainDepartment = Program.Load(Path);
                for(int i=0;i<MainDepartment.Departments.Count;i++)
                {
                    for (int j = 0; j < MainDepartment.Departments[i].Persons.Count; j++)
                    {
                        if(MainDepartment.Departments[i].Persons[j] is Boss)
                        {
                            Boss boss = MainDepartment.Departments[i].Persons[j] as Boss;
                            boss.Department = MainDepartment.Departments[i];
                            MainDepartment.Departments[i].Persons[j] = boss;
                        }
                    }
                }
            }
            else
            {
                Department WithoutDepartment = new Department(NameWithoutDepartment);
                WithoutDepartment.IsMainDepartment = true;
                MainDepartment.Departments.Add(WithoutDepartment);

            }
            
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
            if (currentDep == null)
            {
                PersonsInDepartment.ItemsSource = null;
                return;
            }
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
            Department dep=new Department();
            for (int i = 0; i < MainDepartment.Departments.Count; i++)
            {
                if(MainDepartment.Departments[i].DepartmentName == CurrentPerson.NameDepartment)
                {
                    dep = MainDepartment.Departments[i];
                    break;
                }
            }
            for(int i=0;i<dep.Persons.Count;i++)
            {
                if(dep.Persons[i]==CurrentPerson)
                {
                    dep.Persons.Remove(dep.Persons[i]);
                    break;
                }
            }
            for (int i = 0; i < MainDepartment.Departments.Count; i++)
            {
                for(int j=0;j<MainDepartment.Departments[i].Departments.Count;j++)
                {
                    if(MainDepartment.Departments[i].Departments[j].DepartmentName == dep.DepartmentName)
                    {
                        MainDepartment.Departments[i].Departments[j] = dep;
                    }
                }
                if (MainDepartment.Departments[i].DepartmentName == dep.DepartmentName)
                {
                    MainDepartment.Departments[i] = dep;
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

                    if (MainDepartment.Departments[i].MainDepartmentName == CurrentDepartment.DepartmentName)
                    {
                        //все департаменты у которых главный был удален, носят статус "без деп" т.е. свободных
                        MainDepartment.Departments[i].MainDepartmentName = NameWithoutDepartment;
                        MainDepartment.Departments[i].IsMainDepartment = false;
                    }

                        for (int j = 0; j < MainWindow.MainDepartment.Departments[i].Departments.Count; j++)
                        {
                            if (MainDepartment.Departments[i].Departments[j].MainDepartmentName == CurrentDepartment.DepartmentName)
                            {
                                //все департаменты у которых главный был удален, носят статус "без деп" т.е. свободных
                                MainDepartment.Departments[i].Departments[j].MainDepartmentName = NameWithoutDepartment;
                                MainDepartment.Departments[i].Departments[j].IsMainDepartment = false;
                            }

                            if (MainDepartment.Departments[i].Departments[j].DepartmentName == CurrentDepartment.DepartmentName)
                            {
                                MainDepartment.Departments[i].Departments.Remove(MainDepartment.Departments[i].Departments[j]);

                            }
                        }

                }
                for (int i = 0; i < MainWindow.MainDepartment.Departments.Count; i++)
                {
                    if (MainDepartment.Departments[i].DepartmentName == CurrentDepartment.DepartmentName)
                    {
                        MainDepartment.Departments.Remove(MainDepartment.Departments[i]);
                    }
                }
                //увольнение сотрудников в запас т.е. отправление в депратамент "без деп"
                for (int i = 0; i < CurrentDepartment.Persons.Count; i++)
                {
                    CurrentDepartment.Persons[i].NameDepartment = MainDepartment.Departments[0].DepartmentName;
                    MainDepartment.Departments[0].Persons.Add(CurrentDepartment.Persons[i]);
                    MainDepartment.Departments[0].CountPerson++;
                }
                Program.Save(MainDepartment,Path);
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
            if (CurrentPerson == null) return;
            InfoPersonWindow ShowPersonWindow = new InfoPersonWindow();
            ShowPersonWindow.Owner = this;
            ShowPersonWindow.Title = "Show Person";
            ShowPersonWindow.ButtonCreatePersonCencel.Content = "OK";
            ShowPersonWindow.ButtonCreatePersonOk.Visibility = Visibility.Hidden;

            ShowPersonWindow.ShowDialog();
            RefreshMainWindow();
        }
        private void ListDep_GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            SortingListView(sender, ListDepartments);
        }
        private void DepInDep_GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            SortingListView(sender, DepartmentsInDepartment);
        }

        private void PerInDep_GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            SortingListView(sender, PersonsInDepartment);
        }

        public void SortingListView(object sender, ListView listView)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();

            if (listViewSortCol != null)
            {
                listView.Items.SortDescriptions.Clear();
            }
            ListSortDirection newDir;
            if (_currentTag != sortBy)
            {
                newDir = ListSortDirection.Ascending;
                _currentTag = sortBy;
            }
            else
            {
                newDir = ListSortDirection.Descending;
                _currentTag = "";
            }
            listViewSortCol = column;
            listView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void ListPerson_GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            SortingListView(sender, ListPersons);
        }
    }
}
