using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для InfoPersonWindow.xaml
    /// </summary>
    public partial class InfoPersonWindow : Window
    {
        public InfoPersonWindow()
        {
            InitializeComponent();

            FirstName.Text = MainWindow.CurrentPerson.FirstName;
            LastName.Text = MainWindow.CurrentPerson.LastName;
            YearsCount.Text = MainWindow.CurrentPerson.CountYearsPerson.ToString();
            Experience.Text = MainWindow.CurrentPerson.Experience.ToString();
            NameDepartment.Text = MainWindow.CurrentPerson.NameDepartment;
            Profession.Text = MainWindow.CurrentPerson.Profession;
            Salary.Text = MainWindow.CurrentPerson.Salary.ToString();
        }

        private void ButtonInfoPersonOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.DeleteCurrentPerson();
            Program.Save(MainWindow.MainDepartment);
            Close();
        }

        private void ButtonInfoPersonNO_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
