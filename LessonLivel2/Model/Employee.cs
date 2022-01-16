using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Model
{



    public class Employee: ICloneable
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patranomic { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public Department Department { get; set; }

        public Employee()
        {

        }


        public Employee(string surn, string name, string patron,int age,Department a)
        {
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
