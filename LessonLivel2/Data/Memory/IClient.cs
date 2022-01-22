using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data.Memory
{
    public interface IClient
    {
        Task<ObservableCollection<Employee>> AddEmployee();
        Task<ObservableCollection<Employee>> AddEmployee(bool flag);
        Task<ObservableCollection<Employee>> GetData();
        Task<bool> DeleteData();
        Task<bool> Edit(Employee employee);
        Task<bool> AddDep(Department dep);
        Task<bool> Delete(Employee employee);
        Task<bool> Add(Employee employee);
    }
}
