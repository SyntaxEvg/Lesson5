using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel;
using System.Xml.Serialization;

namespace LessonLivel2.ModelData.Model
{
    public class Department
    {
         [Key] 
        public string DepartName { get; set; }
      
        public virtual ICollection<Employee> Players { get; set; }

       
    }
}