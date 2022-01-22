using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LessonLivel2.ModelData;

namespace LessonLivel2.Data
{
    public class EmpData
    {
        bool FlagMemory = false;
        public EmpData(bool flagMemory)
        {
            FlagMemory = flagMemory;
        }

        public async Task<ObservableCollection<Employee>> AddEmployee()
        {
            var dt = new ObservableCollection<Employee>();

            foreach (var item in  new ListEMP(FlagMemory).GetEnumerator())//извлечить тестовые данные 
            {
               var temp= item as Employee;
               dt.Add(temp);
            }

            return dt;
        }

       


    }
}
