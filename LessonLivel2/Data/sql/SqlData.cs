using LessonLivel2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data.sql
{
    public class SqlData: IData
    {
        public ObservableCollection<Employee> Data = new ObservableCollection<Employee>();

        //UserContext -делаю через using,так как приложению  не требует постоянной открытой бд
        public ObservableCollection<Employee> AddEmployee()
        { 
          
            using (UserContext db = new UserContext())
            {
                var users = db.Users;
                foreach (Employee u in users)
                {
                    Data.Add(u);
                }
               
            }
            if (Data.Count() == 0)//если бд  пустая сначала заполним ее и проинциц.
            {
                Data= Init();
            }
            return Data;
        }

        public ObservableCollection<Employee> Init()
        {
            using (UserContext db = new UserContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false; //Добавляем большое число записей в некоторую таблицу

                foreach (var item in new ListEMP().GetEnumerator())//помещаем в бд  данные
                {
                    db.Users.Add(item);
                }                            
                db.ChangeTracker.DetectChanges();//Обновляем сведения об изменениях. Работает быстро
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
               
            }
            return Data;
        }
    }
}
