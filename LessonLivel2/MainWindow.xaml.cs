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
            ////ControlUP.DataContext= ItemEmployee;
            //ComboDPT.Items.Add(ItemEmployee.Department.DepartName);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           //тут проверка и сохранение обьекта 
        }
    }
}
