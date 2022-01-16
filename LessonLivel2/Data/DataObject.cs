using LessonLivel2.Data.sql;
using LessonLivel2.Model;
using LessonLivel2.SaveConfig;
using LessonLivel2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    internal class DataObject
    {
        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> _Employee
        {
            get {
                if (MainWindowViewModel.flagMemory) // если выбран способ не из базы грузим это
                {
                    employees = new LoadFiles().LoadFile();
                    SaveFile(employees);//save file
                }
                else
                {
                    employees = new SqlData().AddEmployee();//из бд извлекаем
                }
                return employees; 
            }
            set {
                employees = value;
            }
        }

        public void SaveFile(ObservableCollection<Employee> employees)
        {
           
                File.WriteAllText(MainWindowViewModel.Employee, JsonConvert.SerializeObject(employees));
            
        }
    }
}
