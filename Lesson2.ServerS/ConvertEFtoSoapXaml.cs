using EmployeeNew = ModelData.Data.ProviderType.ViewCLassObjectSoap;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using LessonLivel2.ModelData;

namespace Lesson2.Server
{
    internal class ConvertEFtoSoapXaml
    {
        List<Employee> employeeOLD =null;
        public ConvertEFtoSoapXaml(List<Employee> users)
        {
            employeeOLD= users;
        }
       public ObservableCollection<EmployeeNew.Employee> ToEmplXaml()//конвертер типа
        {
         
            ObservableCollection<EmployeeNew.Employee> employeesSoap = new ObservableCollection<EmployeeNew.Employee>(); 
            if (employeeOLD != null)
            {
                foreach (var item in employeeOLD)
                {
                    EmployeeNew.Employee employees =new EmployeeNew.Employee();
                    employees.Id = item.Id;
                    employees.Age = item.Age;
                    employees.Name = item.Name;
                    employees.Patranomic = item.Patranomic;
                    employees.Surname = item.Surname;
                    employees.Department = new EmployeeNew.Department();
                    employees.Department.DepartName = item.Department?.DepartName;                    
                    employeesSoap.Add(employees);
                }
            }
            return employeesSoap;

        }
    }
}