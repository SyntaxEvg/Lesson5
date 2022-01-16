using LessonLivel2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    internal interface IData
    {
        ObservableCollection<Employee> AddEmployee();
    }
}
