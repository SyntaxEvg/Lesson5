using LessonLivel2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    internal interface IData
    {
         Task<ObservableCollection<Employee>> AddEmployee();
        Task<ObservableCollection<Employee>> GetData();
        Task<bool> DeleteData();
        Task<bool> Edit(Employee employee);
        Task<bool> AddDep(Department dep);
        Task<bool> Delete(Employee employee);
        Task<bool> Add(Employee employee);

    }
}
