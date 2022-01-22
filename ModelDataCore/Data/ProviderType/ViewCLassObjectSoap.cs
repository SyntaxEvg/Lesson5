using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelData.Data.ProviderType
{
    public static class ViewCLassObjectSoap
    {
        public class Employee : ICloneable
        {       
            public Guid Id { get; set; }
          
            public string Name { get; set; }
        
            public string Surname { get; set; }
          
            public string Patranomic { get; set; }
         
            public int Age { get; set; }
            public Department Department_DepartName { get; set; }//связь 1 ко многим. именно тут  будет  лежать департамент,а не ниже

            public  Department Department { get; set; }

            public object Clone()
            {
                return MemberwiseClone(); //копия 
            }
        }

        public class Department
        {
           
            public string DepartName { get; set; }

            public  Employee Players { get; set; }


        }

    }
}
