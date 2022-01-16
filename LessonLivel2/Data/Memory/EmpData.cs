using LessonLivel2.Model;
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
    public class EmpData
    {
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public ObservableCollection<Employee> AddEmployee()
        {
            Employees.Add(new Employee("Панкратова", "Мира", "Павловна", 23, new Department("МВД")));
            Employees.Add(new Employee("Антипова", "София", "Егоровна", 27, new Department("Минтруд")));
            Employees.Add(new Employee("Морозов", "Дмитрий", "Иванович", 33, new Department("МЧС")));
            Employees.Add(new Employee("Семенова", "Мария", "Ивановна", 28, new Department("Минтруд")));
            Employees.Add(new Employee("Лазарев", "Александр", "Арсентьевич", 29, new Department("МВД")));
            Employees.Add(new Employee("Власов", "Владимир", "Александрович", 47, new Department("Минтруд")));
            Employees.Add(new Employee("Копылова", "Александра", "Анатольевна", 41, new Department("Минфин")));
            Employees.Add(new Employee("Новиков", "Дамир", "Ярославович", 35, new Department("МИД")));
            Employees.Add(new Employee("Гуров", "Данила", "Платонович", 37, new Department("Минтруд")));
            Employees.Add(new Employee("Меркулов", "Максим", "Артёмович", 25, new Department("Минфин")));
            SaveFile(Employees);
            return Employees;
        }

        public void SaveFile(ObservableCollection<Employee> employees)
        {
            if (!File.Exists(MainWindowViewModel.Employee))
            {
                File.WriteAllText(MainWindowViewModel.Employee, JsonConvert.SerializeObject(employees));
            }
        }


    }
}
