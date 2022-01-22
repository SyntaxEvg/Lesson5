using LessonLivel2.Command;
using LessonLivel2.Data;
using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
//using LessonLivel2.ModelData;
//using LessonLivel2.ModelData.Model;
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
using static ServiceReference.WebServiceSoapClient;
using DataObject = LessonLivel2.Data.DataObject;
//using DataObject = LessonLivel2.Data.DataObject;
using Task = System.Threading.Tasks.Task;
//using DataObject = LessonLivel2.Data.DataObject;

namespace LessonLivel2.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //WebServiceSoapClient webServiceSoapClient = new WebServiceSoapClient(EndpointConfiguration.WebServiceSoap);
        #region Field
        private Employee itemEmployeeTemp;
        private Employee itemEmployee;
        ObservableCollection<Employee> _Employees;
        private string strСhoice;
        private bool _SelectData = false;
        //IData data;//интерф. для работы  бд
        public static bool flagMemory = true;

        public readonly string NameFileList = "Employee.json";
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public  void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));


            }
        }
      
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
       
        public string StrСhoice //просто слово где чекбокс
        {
            get {
                return SelectData ? "Выбрать Memory" : "Выбрать SQL";
                }
            set {
                strСhoice = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                //new Client();
                if (_Employees == null || _Employees.Count()==0)//запрос данных в источнике
                {
                    string dp = null;

                     _Employees =Task.Run(()=>new DataObject(flagMemory, NameFileList)._Employee).GetAwaiter().GetResult();                          

                   // _Employees = new LessonLivel2.Data.DataObject(flagMemory, NameFileList)._Employee;                          
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
                OnPropertyChanged();
            }
        } 
        public ObservableCollection<string> BoxDepar { get; set; }

        public string depert;

        private int myVar;

        public string Depert
        {
            get { return depert; }
            set {
                depert = value; OnPropertyChanged(); 
            }
        }



        public Employee ItemEmployee
        {
            get
            {
                return itemEmployee;
            }
            set
            {
                itemEmployee = value;
                ItemEmployeeTemp= value;
                if (ItemEmployeeTemp !=null)
                {
                    ItemEmployeeTemp = (Employee)ItemEmployee.Clone();
                    Depert = ItemEmployee.Department?.DepartName.Length>0 ? ItemEmployee.Department.DepartName :"null";
                }

                OnPropertyChanged("ItemEmployee");
            }
        }//выбранный элемент
       
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
                Depert = "";
                var t = ItemEmployeeTemp.Clone() as Employee;
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
            var nameDep = Depert.ToLower().Trim(); 

            var t = BoxDepar.Any(x => x.ToLower().Trim().Contains(nameDep));//если сущ.в последовательности 

            if (t == true) return;

                    BoxDepar.Add(nameDep.Trim());//добавление нового вида департамент
                    if (!flagMemory)
                    {
                        var de =new Department { DepartName = nameDep };
                        //Task.Run(() =>data.AddDep(de));
                    }
                
            
        }


        private void ExecuteEditCommand(object obj)
        {
            AddDepartament();
            if (ItemEmployeeTemp.Name == ItemEmployee.Name &&
            ItemEmployeeTemp.Surname == ItemEmployee.Surname &&
                ItemEmployeeTemp.Age == ItemEmployee.Age &&
                ItemEmployeeTemp.Patranomic == ItemEmployee.Patranomic &&
               Depert == ItemEmployee.Department.DepartName)
                return;//если не совпадает  выходимю
           
            var index =Employees.IndexOf(ItemEmployee);
            if (index != -1)
            {
                Employees[index] = ItemEmployeeTemp;//ObservableCollection для редактир получаем индекс с помощью IndexOf

            }
            if (!flagMemory)
            {
              //  Task.Run(() => data.Edit(ItemEmployee)).GetAwaiter();
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
                if (!flagMemory) {
                    //Task.Run(() =>data.Delete(itemEmployee));
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
                Department = new Department {DepartName= Depert },
            };
            var n = Employees.FirstOrDefault(x => Model.Name == x.Name && 
            Model.Surname == x.Surname && 
            Model.Age == x.Age);
            if (n != null) 
                return;

            Employees.Add(Model);
            ItemEmployeeTemp = null;
            if (!flagMemory)
            {
                Task.Run(() => DataObject.client.Add(Model)).GetAwaiter();
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
