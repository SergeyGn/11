﻿using Newtonsoft.Json;
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

            int depInDepCount = 0;
            int perInDepCount = 0;
            for (int i = 0; i < department.Departments.Count; i++) 
            {
                if (department.Departments[i].DepartmentName != MainWindow.NameWithoutDepartment) //чтобы не показывать строчку "без департаментов"
                {
                    departmentsList.Add(department.Departments[i]);
                    depInDepCount++;
                }
            }

                for (int i = 0; i < MainWindow.MainDepartment.Departments.Count; i++)
                {
                    //чтобы нельзя было добавить этот же департамент
                    if (MainWindow.MainDepartment.Departments[i].DepartmentName != MainWindow.CurrentDepartment.DepartmentName)
                    {
                      //чтобы нельзя было добавлять департамент которые во главе текущего департамента
                      if(MainWindow.MainDepartment.Departments[i].DepartmentName!= MainWindow.CurrentDepartment.MainDepartmentName)
                        //чтобы нельзя было добавлять департаменты у которых уже есть главный департамент
                        if (MainWindow.MainDepartment.Departments[i].IsMainDepartment == false)
                        {
                            departmentsList.Add(MainWindow.MainDepartment.Departments[i]);
                        }
                    }
                }

            for (int i = 0; i < persons.Count; i++) 
            {
                personsList.Add(persons[i]);
                perInDepCount++;
            }
            


            
            ListDepartments.ItemsSource = departmentsList;
            for(int i=0;i<depInDepCount;i++)                             //выделяем департаменты которые есть уже в субординации
            {
                ListDepartments.SelectedItem = ListDepartments.Items[i];
            }    
            CreateDepartmentWindow.PersonInDepartment.SelectAll();
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
                    Department dep = ListDepartments.SelectedItems[i] as Department;
                    dep.IsMainDepartment = true;
                    dep.MainDepartmentName = MainWindow.CurrentDepartment.DepartmentName;
                    AddDepartment.Add(dep);
                }

                List<Person> AddPerson = new List<Person>();

                for (int i = 0; i < PersonInDepartment.SelectedItems.Count; i++)
                {
                    for (int j = 0; j < personsList.Count; j++)
                    {
                        if (PersonInDepartment.SelectedItems[i] == personsList[j])
                        {
                            personsList[j].NameDepartment = nameDepartment;
                            AddPerson.Add(personsList[j]);

                        }
                    }
                }


                newDepartment = new Department(nameDepartment);
                newDepartment.Departments = AddDepartment;
                newDepartment.Persons = AddPerson;
                newDepartment.CountDepartment = AddDepartment.Count;
                newDepartment.CountPerson = AddPerson.Count;



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
