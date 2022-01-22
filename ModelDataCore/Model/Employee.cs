using LessonLivel2.ModelData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LessonLivel2.ModelData
{
   
    public class Employee: ICloneable
    {
        /// <summary>
        ///  The duplicate key value is (00000000-0000-0000-0000-000000000000). что  не возникала и был рандом
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }= Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patranomic { get; set; }
        [Required]
        public int Age { get; set; }
        public Department Department_DepartName { get; set; }//связь 1 ко многим. именно тут  будет  лежать департамент,а не ниже
        [Required]
        public virtual Department Department { get; set; }

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

     
        public object Clone()
        {
            return  MemberwiseClone(); //копия 
           
        }
    }
}
