using LessonLivel2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data.sql
{
    public class INItDB
    {
        public void Init()
        {
            using (UserContext db = new UserContext())
            {
               db.Users.Add(new Employee("Панкратова", "Мира", "Павловна", 23, new Department("МВД")));
               db.Users.Add(new Employee("Антипова", "София", "Егоровна", 27, new Department("Минтруд")));
               db.Users.Add(new Employee("Морозов", "Дмитрий", "Иванович", 33, new Department("МЧС")));
               db.Users.Add(new Employee("Семенова", "Мария", "Ивановна", 28, new Department("Минтруд")));
               db.Users.Add(new Employee("Лазарев", "Александр", "Арсентьевич", 29, new Department("МВД")));
               db.Users.Add(new Employee("Власов", "Владимир", "Александрович", 47, new Department("Минтруд")));
               db.Users.Add(new Employee("Копылова", "Александра", "Анатольевна", 41, new Department("Минфин")));
               db.Users.Add(new Employee("Новиков", "Дамир", "Ярославович", 35, new Department("МИД")));
               db.Users.Add(new Employee("Гуров", "Данила", "Платонович", 37, new Department("Минтруд")));
               db.Users.Add(new Employee("Меркулов", "Максим", "Артёмович", 25, new Department("Минфин")));
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
            }
        }
    }
}
