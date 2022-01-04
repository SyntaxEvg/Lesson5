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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;
using LessonLivel2.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LessonLivel2.SaveConfig;

namespace LessonLivel2
{




    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static string Employee = "Employee.json";

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
       
        private Employee itemEmployee;
        public Employee ItemEmployee
        { 
            get
            {
                return itemEmployee;
            }
            set
            {
                itemEmployee=value;
                OnPropertyChanged();
            }
        }//выбранный элемент
        private Employee itemEmployeeTemp;
        public Employee ItemEmployeeTemp
        {
            get
            {
                return itemEmployeeTemp;
            }
            set
            {
                itemEmployeeTemp = value;
                OnPropertyChanged();
            }
        }//в графике



        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
         
        public  MainWindow()
        {
           
            InitializeComponent();
            this.Employees = new LoadFiles().LoadFile();
            foreach (var item in Employees)
            {
                var dp = item.Department.DepartName;//добавляем все различные деп. которые встречали в данных 
                if (!ComboDPT.Items.Contains(dp))
                {
                    ComboDPT.Items.Add(dp);
                }
            }
            DataContext = this;

        }

        private void ListEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemEmployeeTemp= (Employee)ItemEmployee?.Clone();
            //ItemEmployee = null;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           //тут проверка и сохранение обьекта 
        }


        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить запись?","Удаление",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                Employees.Remove(itemEmployee);
                e.Handled = true;
            }
        }

       

        private void Clear_s(object sender, RoutedEventArgs e)
        {
            if (ItemEmployeeTemp !=null)
            {
                Names.Text = ItemEmployeeTemp.Name = "";
                Patranomic.Text = ItemEmployeeTemp.Patranomic = "";
                Surnames.Text = ItemEmployeeTemp.Surname = "";
                Ages.Text = (ItemEmployeeTemp.Age = 0).ToString();
                ItemEmployee = ItemEmployeeTemp;
            }
        }

        private void Save_s(object sender, RoutedEventArgs e)
        {
            // ItemEmployee = ItemEmployeeTemp;//не сохраняет объект

            if (ItemEmployeeTemp.Name == ItemEmployee.Name &&
             ItemEmployeeTemp.Surname == ItemEmployee.Surname &&
                 ItemEmployeeTemp.Age == ItemEmployee.Age &&
                 ItemEmployeeTemp.Patranomic == ItemEmployee.Patranomic &&
                ItemEmployeeTemp.Department.DepartName == ItemEmployee.Department.DepartName)
                return;//если не совпадает  выходимю


                Employees.Add(ItemEmployeeTemp);
                Employees.Remove(ItemEmployee);
                e.Handled = true;
            

        }

        private void Save_new(object sender, RoutedEventArgs e)
        {
            int age;
            int.TryParse(Ages.Text, out age);
            var Model = new Employee()
            {
                 Name= Names.Text,
                 Patranomic= Patranomic.Text,
                 Surname= Surnames.Text,               
                 Age = age,
                 Department=new Department(ComboDPT.SelectedItem.ToString()),
            };

            var n = Employees.FirstOrDefault(x => Model.Name == x.Name && Model.Surname == x.Surname && Model.Age == x.Age);
            if (n != null) return;

            if (Model.Name.Length>0 && Model.Surname.Length>0 && Model.Age>0 && Model.Age<89)
            {
                Employees.Add(Model);
            }
            
        }
    }
}
