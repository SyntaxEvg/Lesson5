//using LessonLivel2.ModelData;
//using LessonLivel2.ModelData.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static ModelData.Data.ProviderType.ViewCLassObjectSoap;
namespace LessonLivel2.Data
{
    public interface IData
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
