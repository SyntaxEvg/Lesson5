using LessonLivel2.Data;
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

namespace LessonLivel2.SaveConfig
{
    public class LoadFiles 
    {
        public ObservableCollection<Employee> LoadFile()
        {
            if (File.Exists(MainWindowViewModel.Employee))
            {
                using (FileStream fs = new FileStream(MainWindowViewModel.Employee, FileMode.Open))
                {
                    using (StreamReader fa = new StreamReader(fs))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return (ObservableCollection<Employee>)serializer.Deserialize(fa, typeof(ObservableCollection<Employee>));
                        
                    }
                }
            }
            else
            {                
              return  new DataObject()._Employee;
            }
        }
       
    }
}
