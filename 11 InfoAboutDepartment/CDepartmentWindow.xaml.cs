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
        List<Department> AddDepartment = new List<Department>();
        public CDepartmentWindow()
        {
            ObservableCollection<Department> departments=new ObservableCollection<Department>();
            InitializeComponent();
         for(int i=0;i<MainWindow.MainDepartment.Departments.Count;i++)
         {
             departments.Add(MainWindow.MainDepartment.Departments[i]);
         }
            ListDepartments.ItemsSource = departments;
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

                for (int i = 0; i < ListDepartments.SelectedItems.Count; i++)
                {
                    AddDepartment.Add(ListDepartments.SelectedItems[i] as Department);
                }                   

                newDepartment = new Department(nameDepartment);
                newDepartment.Departments = AddDepartment;
                newDepartment.CountDepartment = AddDepartment.Count;
                MainWindow.MainDepartment.Departments.Add(newDepartment);
                Program.Save(MainWindow.MainDepartment);
                ListDepartments.Items.Refresh();
                Close();
            }

        }

        private void ButtonCreateDepartmentCencel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
