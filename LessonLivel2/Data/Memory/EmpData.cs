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
      
        public async Task<ObservableCollection<Employee>> AddEmployee()
        {
            var dt = new ObservableCollection<Employee>();

            foreach (var item in  new ListEMP().GetEnumerator())//извлечить тестовые данные 
            {
               var temp= item as Employee;
               dt.Add(temp);
            }

            return dt;
        }

       


    }
}
