using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LessonLivel2.Model
{
    public class Department
    {
         [Key] 
        public string DepartName { get; set; }

         public virtual ICollection<Employee> Players { get; set; }

       
    }
}