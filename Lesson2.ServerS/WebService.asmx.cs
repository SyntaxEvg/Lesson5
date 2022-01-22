
using LessonLivel2.Data;
using LessonLivel2.Data.sql;
using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using ModelData.Data.ProviderType;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Services;

namespace Lesson2.Server
{
    /// <summary>
    /// Сводное описание для WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService//, IData
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }

        public ObservableCollection<ViewCLassObjectSoap.Employee> Data = new ObservableCollection<ViewCLassObjectSoap.Employee>();

        [WebMethod]
        public async Task<ObservableCollection<ViewCLassObjectSoap.Employee>> GetData()
        {
            using (UserContext db = new UserContext())
            {
                List<Employee> users = await db.Users.Include((x) => x.Department).ToListAsync().ConfigureAwait(false); //выгружаю всю связь 
                var t =new ConvertEFtoSoapXaml(users).ToEmplXaml();
                return t;
            }
            return null;
        }

        //[WebMethod]
        //public async Task<bool> DeleteData()//удалить все не реализовано,
        //{
        //    using (UserContext db = new UserContext())
        //    {
        //        var users = await db.Users.ToListAsync();
        //        db.Users.RemoveRange(users);
        //        await db.SaveChangesAsync();
        //        return true;
        //    }
        //}

        //[WebMethod]
        //public async Task<bool> Edit(Employee employee)
        //{

        //    using (UserContext db = new UserContext())
        //    {
        //        var users = await db.Users.FirstOrDefaultAsync((x) => employee.Id == x.Id);//для примера по  айди ))
        //        if (users != null)
        //        {
        //            users= new ConvertEFtoSoapXaml(employee).ToEmplXaml();
        //           // users = employee;
        //            await db.SaveChangesAsync();
        //        }
        //        else return false;
        //    }
        //    return true;
        //}

        //[WebMethod]
        //public async Task<bool> AddDep(Department dep)
        //{
        //    using (UserContext db = new UserContext())
        //    {
        //        var departmentNew = await db.Department.FirstOrDefaultAsync((x) => dep.DepartName.ToLower().Trim() == x.DepartName.ToLower().Trim());
        //        if (departmentNew == null)
        //        {
        //            db.Department.Add(dep);
        //            await db.SaveChangesAsync();

        //        }
        //        else return false;
        //    }
        //    return true;
        //}

        //[WebMethod]
        //public async Task<bool> Delete(Employee employee)
        //{

        //    using (UserContext db = new UserContext())
        //    {
        //        var users = await db.Users.FirstOrDefaultAsync((x) => employee.Id == x.Id);
        //        if (users != null)
        //        {
        //            users = employee;
        //            db.Users.Remove(users);
        //            await db.SaveChangesAsync();

        //        }
        //        else return false;
        //    }
        //    return true;
        //}

        //[WebMethod]
        //public async Task<bool> Add(Employee employee)
        //{

        //    using (UserContext db = new UserContext())
        //    {
        //        var users = await db.Users.FirstOrDefaultAsync((x) => employee.Id == x.Id);
        //        if (users == null)
        //        {
        //            db.Users.Add(employee);
        //            await db.SaveChangesAsync();
        //        }
        //        else return false;
        //    }
        //    return true;
        //}

        ////UserContext -делаю через using,так как приложению  не требует постоянной открытой бд
        [WebMethod]
        public async Task<ObservableCollection<ViewCLassObjectSoap.Employee>> AddEmployee(bool flagMemory)
        {
            await GetData();//запрос данных
            if (Data.Count() == 0)//если бд  пустая сначала заполним ее и проинциц.
            {
                return await Init(flagMemory);
            }
            return Data;
        }


        public async Task<ObservableCollection<ViewCLassObjectSoap.Employee>> Init(bool flagMemory)
        {

            using (UserContext db = new UserContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false; //Добавляем большое число записей в некоторую таблицу


                List<Employee> employees = new List<Employee>();//собираем всех юзеров что поступили к нам
                foreach (var collection in new ListEMP(flagMemory).GetEnumerator())//помещаем в бд  данные
                {

                    if (collection is List<Department>)
                    {
                        var colDep = collection as List<Department>;
                        var DEpLocal = await db.Department.ToListAsync();
                        foreach (var department in colDep)
                        {
                            var Dexists = DEpLocal.FirstOrDefault(x => x.DepartName == department.DepartName);
                            if (Dexists == null)
                            {
                                db.Department.Add(department);
                            }
                        }
                    }
                    else if (collection is List<Employee>)
                    {
                        var colEmp = collection as List<Employee>;
                        var UserLocal = db.Users.ToList();
                        foreach (var item in colEmp)
                        {
                            var Userexists = UserLocal.FirstOrDefault(x => x.Id == item.Id);
                            if (Userexists == null)
                            {
                                employees.Add(item);//промеж.хранение
                            }
                            //сорт все и
                            // когда закончили сохранили
                            // сначала базу департаментов  и поместили в лист
                            // Юзеров для дальнейшего распределния

                        }
                    }

                }
                db.ChangeTracker.DetectChanges();//Обновляем сведения об изменениях. Работает быстро
                try
                {
                    db.SaveChanges();
                    //раскидаем их рандомно
                    var rand = new Random();
                    var dep = db.Department.ToList();

                    foreach (var item in employees)//помещаем в бд  данные
                    {
                        var emp = item;
                        int ind = rand.Next(dep.Count);
                        emp.Department = dep[ind];
                        db.Users.Add(emp);//при первом создании бд  данные деп. будут рандомные
                    }
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    //if (MessageBox.Show($"Да - выйти из приложения {ex.StackTrace}", "ErrorDB",
                    //     MessageBoxButton.YesNo,
                    //     MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //{

                    //    Environment.Exit(0);
                    //}
                   
                    return Data;// empty return
                }
            }
            await GetData();//запрос данных
            return Data;
        }

        public Task<ObservableCollection<Employee>> AddEmployee()
        {
            throw new NotImplementedException();//
        }
    }  
}
