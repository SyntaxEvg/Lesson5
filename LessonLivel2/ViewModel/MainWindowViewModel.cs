using LessonLivel2.Command;
using LessonLivel2.Data;
using LessonLivel2.Data.sql;
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
        IData data;
        public static bool flagMemory = true;

        public static string Employee = "Employee.json";
        public async Task OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
               await Task.Run(() => PropertyChanged(this, new PropertyChangedEventArgs(prop)));


            }
        }
        public  event PropertyChangedEventHandler? PropertyChanged;


        private bool _SelectData=false;
        public bool SelectData// что выбрано sql or mem
        {
            get
            {
                return _SelectData;
            }
            set
            {
                _SelectData = value;
                flagMemory = value;
                OnPropertyChanged();

                OnPropertyChanged("StrСhoice");
            }
        }

        private string strСhoice;
        public string StrСhoice //просто слово где чекбокс
        {
            get {
                return SelectData==true ? "Выбрать Memory" : "Выбрать SQL";
                }
            set {
                strСhoice = value;
                OnPropertyChanged();
            }
        }



        ObservableCollection<Employee> _Employees;
        public   ObservableCollection<Employee> Employees
        {
            get
            {
                
                if (_Employees == null || _Employees.Count()==0)//запрос данных в источнике
                {
                    string dp = null;
                    _Employees =Task.Run(()=>new LessonLivel2.Data.DataObject()._Employee).Result;                          
                    foreach (var item in _Employees)
                    {
                        
                        dp = item.Department.DepartName;//добавляем все различные деп. которые встречали в данных 
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
            set
            {
                _Employees = value;
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
                ItemEmployeeTemp= value;//метод не клон всю глубину ,костыль))) 
                if (ItemEmployeeTemp !=null)
                {// этот танец нужен, для того,чтобы работать  с клон обьекта ,
                 // и изменения не принимались до того времени пока не будет  сохранен
                    ItemEmployeeTemp.Surname=value.Surname;
                    ItemEmployeeTemp.Department=value.Department;
                    ItemEmployeeTemp.Age=value.Age;
                    itemEmployeeTemp.Patranomic=value?.Patranomic;
                    itemEmployeeTemp.Name=value.Name;
                }
                
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
        RelayCommand updateSourse;       
        RelayCommand _addClientCommand;
        RelayCommand _ClearCommand;
        RelayCommand _DeleteCommand;
        RelayCommand _EditCommand;

        public  ICommand UpdateSourse => updateSourse ??= new RelayCommand( PerformUpdateSourse);

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
                    if (!flagMemory)
                    {
                        var de =new Department { DepartName = nameDep };
                        Task.Run(() => new SqlData().AddDep(de));
                    }
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
            if (!flagMemory)
            {
                Task.Run(() => data.Edit(ItemEmployee)).GetAwaiter();
            }
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
                if(!flagMemory) {
                    Task.Run(() => new SqlData().Delete(itemEmployee));
                                }

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
                Department = new Department {DepartName= ItemEmployeeTemp.Department.DepartName },
            };
            var n = Employees.FirstOrDefault(x => Model.Name == x.Name && Model.Surname == x.Surname && Model.Age == x.Age);
            if (n != null) 
                return;

            Employees.Add(Model);
            ItemEmployeeTemp = null;
            if (!flagMemory)
            {
                Task.Run(() => data.Add(Model)).GetAwaiter();
            }
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
        }


        public bool CanExecuteAddClientCommand(object parameter)//кнопка будет доступна только тогда, когда поля будут введены 
        {
            return Save_EditBool();
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

       
      
        private  void PerformUpdateSourse(object commandParameter)
        {
            string str = null;
            if (!StrСhoice.Contains("Memory"))
            {
                flagMemory = true;
                str = "Memory";
            }
            else { flagMemory = false; str = "Sql"; }

            if (MessageBox.Show($"Обновить источник данных на {str}", "UpdateSourse",
              MessageBoxButton.YesNo,
              MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Employees.Clear();
                OnPropertyChanged("Employees");//вызов свойства для обновление данных 
            }


            
        }
    }
}
