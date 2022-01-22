using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using System.Data.Entity;

namespace LessonLivel2.Data.sql
{
   public  class UserContext : DbContext
    {
        //public  UserContext(): base("Name=DbSyntaxEvgen")
        //{
        //    //this.Configuration.ProxyCreationEnabled = false; 
        //}
        public UserContext() : base("DbSyntaxEvgen")
        {
            //this.Configuration.ProxyCreationEnabled = false; 
        }

        public DbSet<Employee> Users { get; set; }
        public DbSet<Department> Department { get; set; }
     
    }
}
