using LessonLivel2.Command;
using LessonLivel2.Model;
using LessonLivel2.SaveConfig;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LessonLivel2.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public static string Employee = "Employee.json";
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (_Employees == null)
                {
                    _Employees = new LoadFiles().LoadFile();
                    foreach (var item in Employees)
                    {
                        var dp = item.Department.DepartName;//добавляем все различные деп. которые встречали в данных 
                        if (BoxDepar == null)
                        {
                            BoxDepar = new ObservableCollection<string>();
                        }
                        
                        if (!BoxDepar.Contains(dp))
                        {
                            BoxDepar.Add(dp);
                        }
                    }
                }
                return _Employees;
            }
        } 
        public ObservableCollection<string> BoxDepar { get; set; }
        
        private Employee itemEmployee;
        public Employee ItemEmployee
        {
            get
            {
                return itemEmployee;
            }
            set
            {
                itemEmployee = value;
                ItemEmployeeTemp=value;
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

        #region ICommand список команд для view
        RelayCommand _addClientCommand;
        RelayCommand _ClearCommand;
        RelayCommand _DeleteCommand;
        RelayCommand _EditCommand;
        public ICommand AddClient
        {
            get
            {
                if (_addClientCommand == null)
                    _addClientCommand = new RelayCommand(ExecuteAddClientCommand, CanExecuteAddClientCommand);
                return _addClientCommand;
            }
        }
        public ICommand Clear
        {
            get
            {
                if (_ClearCommand == null)
                    _ClearCommand = new RelayCommand(ExecuteClearCommand, CanExecuteClearCommand);
                return _ClearCommand;
            }
        }
        public ICommand Edit
        {
            get
            {
                if (_EditCommand == null)
                    _EditCommand = new RelayCommand(ExecuteEditCommand, CanExecuteEditCommand);
                return _EditCommand;
            }
        }
        public ICommand Delete
        {
            get
            {
                if (_DeleteCommand == null)
                    _DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
                return _DeleteCommand;
            }
        }

        private bool CanExecuteClearCommand(object obj)
        {
            if (ItemEmployeeTemp == null)
            {
                return false;
            }
            return true;
        }

        private void ExecuteClearCommand(object obj)
        {
            if (ItemEmployeeTemp != null)
            {
                 ItemEmployeeTemp.Name = "";
                ItemEmployeeTemp.Patranomic = "";
                ItemEmployeeTemp.Surname = "";
                (ItemEmployeeTemp.Age = 0).ToString();
                ItemEmployeeTemp.Department.DepartName = "";
                var t =ItemEmployeeTemp.Clone() as Employee;
                ItemEmployeeTemp=t;
                t = null;
            }
        }

        
        #endregion
       

        private bool CanExecuteEditCommand(object obj)
        {
            return Save_EditBool();
        }

        void AddDepartament()
        {
            var nameDep = ItemEmployeeTemp.Department.DepartName;
            foreach (var item in BoxDepar)
            {
                if (!item.Contains(nameDep, StringComparison.OrdinalIgnoreCase))
                {
                    BoxDepar.Add(nameDep);//добавление нового вида департамент
                    break;
                }
            }
        }


        private void ExecuteEditCommand(object obj)
        {
            AddDepartament();
            if (ItemEmployeeTemp.Name == ItemEmployee.Name &&
            ItemEmployeeTemp.Surname == ItemEmployee.Surname &&
                ItemEmployeeTemp.Age == ItemEmployee.Age &&
                ItemEmployeeTemp.Patranomic == ItemEmployee.Patranomic &&
               ItemEmployeeTemp.Department.DepartName == ItemEmployee.Department.DepartName)
                return;//если не совпадает  выходимю


            Employees.Add(ItemEmployeeTemp);
            Employees.Remove(ItemEmployee);

        }

      
        private bool CanExecuteDeleteCommand(object obj)//разрешен на удален.
        {
            if (ItemEmployee == null)
            {               
                return false;
            }
            return true;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            if (MessageBox.Show("Удалить запись?", "Удаление",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Employees.Remove(itemEmployee);
            }
        }

        public void ExecuteAddClientCommand(object parameter)
        {
            AddDepartament();
            var Model = new Employee()
            {
                Name = ItemEmployeeTemp.Name,
                Patranomic = ItemEmployeeTemp.Patranomic,
                Surname = ItemEmployeeTemp.Surname,
                Age = ItemEmployeeTemp.Age,
                Department = new Department(ItemEmployeeTemp.Department.DepartName),
            };
            var n = Employees.FirstOrDefault(x => Model.Name == x.Name && Model.Surname == x.Surname && Model.Age == x.Age);
            if (n != null) 
                return;

            Employees.Add(Model);
            ItemEmployeeTemp = null;
        }

        bool Save_EditBool()
        {
            if (ItemEmployeeTemp != null)
            {
                int age;
                int.TryParse(ItemEmployeeTemp.Age.ToString(), out age);
                if (age == 0 && age == 0 && ItemEmployeeTemp.Age > 89)//фильтрация возраста
                {
                    return false;
                }

                if (string.IsNullOrEmpty(ItemEmployeeTemp.Name) || string.IsNullOrEmpty(ItemEmployeeTemp.Surname))
                    return false;
            }
            return true;
        }//тавтологие кода на проверку содержим.


        public bool CanExecuteAddClientCommand(object parameter)//кнопка будет доступна только тогда, когда поля будут введены 
        {
            return Save_EditBool();
        }
    }
}
