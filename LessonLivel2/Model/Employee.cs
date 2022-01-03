using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Model
{



    public class Employee: ICloneable
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patranomic { get; set; }
        public int Age { get; set; }
        public Department Department { get; set; }

        public Employee()
        {

        }


        public Employee(string surn, string name, string patron,int age,Department a)
        {
           // Id = id;
            Name = name;
            Surname = surn;
            Age = age;
            Patranomic = patron;
            Age = age;
            Department = a;
          
        }

       
        public  object Clone()
        {
            return MemberwiseClone(); //копия 
           
        }
    }
}
