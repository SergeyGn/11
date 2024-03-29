﻿using System;
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
    /// Логика взаимодействия для CPersonWindow.xaml
    /// </summary>
    public partial class CPersonWindow : Window
    {
        // здесь нужно  реализовать поиск всех наследников от класса Person, но пока так
        string[] professions = new string[] { "интерн", "рабочий", "начальник" };

        public CPersonWindow()
        {
            InitializeComponent();            

            for(int i=0;i<professions.Length;i++)
            {
                Profession.Items.Add(professions[i]);
            }
            for(int i=0;i<MainWindow.MainDepartment.Departments.Count;i++)
            {
                NameDepartment.Items.Add(MainWindow.MainDepartment.Departments[i].DepartmentName);
            }

            
            CountDay.Maximum = DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);
            CountDay.Minimum = 0;
            
        }

        private void ButtonCreatePersonOk_Click(object sender, RoutedEventArgs e)
        {
            string firstName="";
            string lastName="";
            DateTime dateBirth=DateTime.MinValue;
            DateTime dateEmployment=DateTime.MinValue;
            string nameDepartment="";
            Person newPerson=new Intern("","",DateTime.Now,DateTime.Now,"");
            if (LastName.Text != "")
            {
                if (FirstName.Text != "")
                {
                    if (Program.CheckDate(DateBirth.Text))
                    {
                        if (Program.CheckDate(DateEmployment.Text))
                        {

                            if (NameDepartment.Text != "")
                            {
                                firstName = FirstName.Text;
                                lastName = LastName.Text;
                                dateBirth = DateTime.Parse(DateBirth.Text);
                                dateEmployment = DateTime.Parse(DateEmployment.Text);
                                nameDepartment = NameDepartment.Text;
                                InfoPanel.Text = "идет сохранение";
                            }
                            else
                            {
                                InfoPanel.Text = "Укажите Департамент";
                            }
                        }
                        else
                        {
                            InfoPanel.Text = "Неверный формат даты для поля (Дата приема на работу)";

                        }
                    }
                    else
                    {
                        InfoPanel.Text = "Неверный формат даты для поля (Дата рождения)";
                    }
                }
                else
                {
                    InfoPanel.Text = "Введите имя";
                }
            }    // проверка на пустые поля
            else
            {
                InfoPanel.Text = "Введите фамилию";
            }

            if (InfoPanel.Text == "идет сохранение")
            {
                Department dep=null;

                for (int i = 0; i < MainWindow.MainDepartment.Departments.Count; i++)
                {
                    if (MainWindow.MainDepartment.Departments[i].DepartmentName == nameDepartment)
                    {
                        dep = MainWindow.MainDepartment.Departments[i];
                        break;
                    }
                }

                switch (Profession.Text)
                {
                    case "интерн":
                        newPerson = new Intern(firstName, lastName, dateBirth, dateEmployment, nameDepartment);
                        break;
                    case "рабочий":
                        newPerson = new Employee(firstName, lastName, dateBirth, dateEmployment, nameDepartment, int.Parse(ValueSlider.Text));
                        break;
                    case "начальник":
                        //проверка есть ли босс в этом департаменте
                        if (Title != "Edit Person")
                        {
                            if (dep.Persons.Count > 0)
                            {
                                for (int i = 0; i < dep.Persons.Count; i++)
                                {
                                    if (dep.Persons[i] is Boss)
                                    {
                                        InfoPanel.Text = "Уже есть начальник в выбранном департаменте";
                                        return;
                                    }
                                }
                            }
                        }
                        newPerson = new Boss(firstName, lastName, dateBirth, dateEmployment, nameDepartment,dep);
                        break;
                    default:
                        InfoPanel.Text = "Выберите специальность";
                        return;
                }
                //если редактируем то нужно переопределить человека
                if (Title == "Edit Person")
                {
                    if (dep.Persons.Count > 0)
                    {
                        for (int i = 0; i < dep.Persons.Count; i++)
                        {
                            if(dep.Persons[i]==MainWindow.CurrentPerson)
                            {
                                dep.Persons[i] = newPerson;
                            }
                        }
                    }
                }
                else
                {
                    dep.Persons.Add(newPerson);
                }
                for (int i = 0; i < MainWindow.MainDepartment.Departments.Count; i++)
                {
                    for(int j=0;j<MainWindow.MainDepartment.Departments[i].Departments.Count; j++)
                    {
                        if (MainWindow.MainDepartment.Departments[i].Departments[j].DepartmentName == nameDepartment)
                        {
                            MainWindow.MainDepartment.Departments[i].Departments[j] = dep;
                            MainWindow.MainDepartment.Departments[i].Departments[j].CountPerson=MainWindow.MainDepartment.Departments[i].Departments[j].Persons.Count;
                        }
                    }
                    if (MainWindow.MainDepartment.Departments[i].DepartmentName == nameDepartment)
                    {
                        MainWindow.MainDepartment.Departments[i] = dep;
                        MainWindow.MainDepartment.Departments[i].CountPerson= MainWindow.MainDepartment.Departments[i].Persons.Count;
                        break;
                    }
                }
                InfoPanel.Text = "идет сохранение";
                Program.Save(MainWindow.MainDepartment, MainWindow.Path);
                Close();
            }

            if (Title=="Edit Person")
            {
                MainWindow.DeleteCurrentPerson();
            }
        }


        private void ButtonCreatePersonCencel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CountDay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueSlider.Text = ((int)CountDay.Value).ToString();
        }

        private void DateBirth_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DateBirth.Text = "";
            DateBirth.Foreground = Brushes.Black;
        }

        private void DateEmployment_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DateEmployment.Text = "";
            DateEmployment.Foreground = Brushes.Black;
        }

        private void Profession_DropDownClosed(object sender, EventArgs e)
        {
            if (Profession.Text == "рабочий")
            {
                CountDay.IsEnabled = true;
                ValueSlider.Text = ((int)CountDay.Value).ToString(); // чтобы поле не было пустым после назначение рабочего в комбобоксе
            }
            else
            {
                CountDay.IsEnabled = false;
                ValueSlider.Text = "";
            }
        }
        
    }
}
