using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;




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
                //чтобы не показывать строчку "без департаментов"
                if (department.Departments[i].DepartmentName != MainWindow.NameWithoutDepartment) 
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
            if (MainWindow.CurrentDepartment.DepartmentName != MainWindow.NameWithoutDepartment)
            {
                for (int i = 0; i < persons.Count; i++)
                {
                    persons[i].NameDepartment = "не определено";
                    personsList.Add(persons[i]);
                    perInDepCount++;
                }
            }
            //люди без департамента
            for(int i=0; i<MainWindow.MainDepartment.Departments[0].Persons.Count; i++)
            {
                MainWindow.MainDepartment.Departments[0].Persons[i].NameDepartment = "не определено";
                personsList.Add(MainWindow.MainDepartment.Departments[0].Persons[i]);
            }
            ListDepartments.ItemsSource = departmentsList;
            for(int i=0;i<depInDepCount;i++)                             //выделяем департаменты которые есть уже в субординации
            {
                ListDepartments.SelectedItems.Add(ListDepartments.Items[i]);
            }            
            PersonInDepartment.ItemsSource = personsList;
            for (int i = 0; i < perInDepCount; i++)                             //выделяем людей которые есть уже в субординации
            {
                PersonInDepartment.SelectedItems.Add(PersonInDepartment.Items[i]);
            }
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
                 // Для того чтобы не выбранные департаменты имели статус свободных
                for (int i = 0; i < ListDepartments.Items.Count; i++)    
                {
                    Department dep = ListDepartments.Items[i] as Department;
                    dep.IsMainDepartment = false;
                    dep.MainDepartmentName = "не определено";
                }

                for (int i = 0; i < ListDepartments.SelectedItems.Count; i++)
                {
                    Department dep = ListDepartments.SelectedItems[i] as Department;
                    dep.IsMainDepartment = true;
                    dep.MainDepartmentName = MainWindow.CurrentDepartment.DepartmentName;
                    AddDepartment.Add(dep);
                }


                List<Person> AddPerson = new List<Person>();
                List<Person> NotAddPerson = new List<Person>();

                if (PersonInDepartment.SelectedItems.Count == 0) //если нечего не выбрано то все идет в список без деп
                {
                    for (int j = 0; j < personsList.Count; j++)
                    {
                        personsList[j].NameDepartment = MainWindow.NameWithoutDepartment;
                        NotAddPerson.Add(personsList[j]); //добавляем все не выделенные в список
                    }
                }
                else
                {
                    for (int i = 0; i < PersonInDepartment.SelectedItems.Count; i++) // если чел.выделен добавляем его в список выдел
                    {
                        Person person = PersonInDepartment.SelectedItems[i] as Person;
                        person.NameDepartment = nameDepartment;
                        AddPerson.Add(person);
                    }
                    //в списке остались только не выделенные
                    for (int j = 0; j < personsList.Count; j++)
                    {
                        if (personsList[j].NameDepartment != nameDepartment) //делаем проверку для того чтобы отбросить переопределенные
                        {
                            NotAddPerson.Add(personsList[j]); //добавляем все не выделенные в список
                        }
                    }
                }
                    //собираем получившийся департамент
                    newDepartment = new Department(nameDepartment);
                newDepartment.Departments = AddDepartment;
                newDepartment.Persons = AddPerson;
                newDepartment.CountDepartment = AddDepartment.Count;
                newDepartment.CountPerson = AddPerson.Count;

                MainWindow.MainDepartment.Departments[0].Persons = NotAddPerson; //список тех кого не выбрали отправляем в без деп;
                //MainWindow.MainDepartment.Departments[0].CountPerson = MainWindow.MainDepartment.Departments[0].Persons.Count;

                if (CreateDepartmentWindow.Title == "Edit Department") //если мы редактируем то значит нужно во всех деп заменить этот деп
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
                }
                else //если создаем то добавляем деп
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
