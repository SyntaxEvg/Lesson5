
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LessonLivel2.SaveConfig;
using LessonLivel2.Data.Memory;
using LessonLivel2.ModelData;

namespace LessonLivel2.Data
{/// <summary>
/// Внимание , чтобы не было ошибок при запуске, выполнить команды: dotnet dev-certs https --clean  dotnet dev-certs https --trust
/// </summary>
    public class DataObject
    {
       public static IClient client= new Client();
        private ObservableCollection<Employee> employees;

        bool FlagMem=false;
        string NameFile = "";
        //public ObservableCollection<Employee> EmployeesRaspedel(bool flagMemory)
        //{
        //    FlagMem = flagMemory;
        //}
        public DataObject(bool flagMemory,string nameFile)
        {
            FlagMem = flagMemory;
            NameFile = nameFile;
        }


        public ObservableCollection<Employee> _Employee
        {
            get {
                if (FlagMem) // если выбран способ не из базы грузим это
                {
                    employees =   new LoadFiles(FlagMem, NameFile).LoadFile();
                    SaveFile(employees);//save file
                }
                else
                {
                    employees = System.Threading.Tasks.Task.Run(()=> client.GetData()).GetAwaiter().GetResult();//получаем с сервера 
                    return employees;
                }
                return employees; 
            }
            set {
                employees = value;
            }
        }

        public  void SaveFile(ObservableCollection<Employee> employees)
        {
           
                File.WriteAllText(NameFile, JsonConvert.SerializeObject(employees));
            
        }
    }
}
