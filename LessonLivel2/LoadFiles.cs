using LessonLivel2.Data;
using LessonLivel2.ModelData;
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
        string NameFile = null;
        bool flag=false;
        public LoadFiles(bool Flag, string namef)
        {
            NameFile = namef;
            this.flag = Flag;
        }

        public ObservableCollection<Employee> LoadFile()
        {
            if (File.Exists(NameFile))
            {
                using (FileStream fs = new FileStream(NameFile, FileMode.Open))
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
              return System.Threading.Tasks.Task.Run(()=> new EmpData(flag).AddEmployee()).GetAwaiter().GetResult();
            }
        }
       
    }
}
