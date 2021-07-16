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

        public MainWindow()
        {
            if (File.Exists(Program.path))
            {
                MainDepartment = Program.Load();
            }

            InitializeComponent();
            ListDepartments.ItemsSource = MainDepartment.Departments;
            
             
        }

        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            CDepartmentWindow CreateDepartmentWindow = new CDepartmentWindow();
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
           ListPersons.ItemsSource = currentDep.Persons;
           DepartmentsInDepartment.ItemsSource = currentDep.Departments;
        }

        private void DepartmentsInDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Department currentDep = new Department();
            currentDep = DepartmentsInDepartment.SelectedItem as Department;
            PersonsInDepartment.ItemsSource = currentDep.Persons;
        }

        private void RefreshMainWindow()
        {
            ListDepartments.Items.Refresh();
            DepartmentsInDepartment.Items.Refresh();
            ListPersons.Items.Refresh();
            PersonsInDepartment.Items.Refresh();
        }
    }

}
