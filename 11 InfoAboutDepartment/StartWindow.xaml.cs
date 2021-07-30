using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public static bool IsTest;
        public static string fullPath;
        public StartWindow()
        {
            InitializeComponent();
            DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName);
            fullPath = directoryInfo.FullName;
            Program.TestFilePath = fullPath + Program.TestFilePath;
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            IsTest = true;
            CreateMainWindow();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            IsTest = false;
            CreateMainWindow();
        }

        private void CreateMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
