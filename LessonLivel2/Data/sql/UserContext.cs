using LessonLivel2.Model;
using System.Data.Entity;

namespace LessonLivel2.Data.sql
{
    class UserContext : DbContext
    {
        public  UserContext(): base("DbSyntaxEvgen")
        {
           
        }

        public DbSet<Employee> Users { get; set; }
        public DbSet<Department> Department { get; set; }

    }
}
