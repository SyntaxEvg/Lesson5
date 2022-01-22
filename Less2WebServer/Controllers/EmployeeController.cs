using Lesson2.Server;
using LessonLivel2.Data;
using LessonLivel2.Data.sql;
using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using Microsoft.AspNetCore.Mvc;
using ModelData.Data.ProviderType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Less2WebServer.Controllers
{
    public class EmployeeController : Controller
    {
        static UserContext db = new UserContext();
        ObservableCollection<ViewCLassObjectSoap.Employee> Data = new();

        [HttpGet]
        [Route("api/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/GetData")]
        public async Task<JsonResult> GetData()
        {
            //using (UserContext db = new UserContext())
            //{
                List<Employee> users = await db.Users.Include((x) => x.Department).ToListAsync().ConfigureAwait(false); //выгружаю всю связь 
                var t = new ConvertEFtoSoapXaml(users).ToEmplXaml();
                return Json(t);

            //}
        }


        [Route("api/DeleteData")]
        public async Task<JsonResult> DeleteData()//удалить все не реализовано,
        {
            using (UserContext db = new UserContext())
            {
                var users = await db.Users.ToListAsync();
                db.Users.RemoveRange(users);
                await db.SaveChangesAsync();

                return Json(true);
            }
        }

        [Route("api/Edit")]
        public async Task<JsonResult> Edit(Employee employee)
        {
            //var tg = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(products1);

            List<Employee> test = new List<Employee>();
            test.Add(employee);
            using (UserContext db = new UserContext())
            {
                var users = await db.Users.FirstOrDefaultAsync((x) => employee.Id == x.Id);//для примера по  айди ))
                if (users != null)
                {
                    var users1 = new ConvertEFtoSoapXaml(test).ToEmplXaml();
                    // users = employee;
                    await db.SaveChangesAsync();
                }
                else return Json(false);
            }
            return Json(true);
        }

        [Route("api/AddDep")]
        public async Task<JsonResult> AddDep(Department dep)
        {
            using (UserContext db = new())
            {
                var departmentNew = await db.Department.FirstOrDefaultAsync((x) => dep.DepartName.ToLower().Trim() == x.DepartName.ToLower().Trim());
                if (departmentNew == null)
                {
                    db.Department.Add(dep);
                    await db.SaveChangesAsync();

                }
                else return Json(false);
            }
            return Json(true);
        }

        [Route("api/Delete")]
        public async Task<JsonResult> Delete(Employee employee)//working
        {

            //using (UserContext db = new UserContext())
            //{
                var users = await db.Users.FirstOrDefaultAsync((x) => employee.Id == x.Id);
                if (users != null)
                {
                    users = employee;
                    db.Users.Remove(users);
                     db.SaveChanges();

                }
                else return Json(false);
            //}
            return Json(true);
        }

        [Route("api/Add")] [HttpPost]
        public async Task<JsonResult> Add([FromBody] string employee)
        {

            var emp = JsonConvert.DeserializeObject<Employee>(employee);

            //using (UserContext db = new UserContext())
            //{
                var users = await db.Users.FirstOrDefaultAsync((x) => emp.Id == x.Id);
                if (users == null)
                {

                    db.Users.Add(emp);
                    db.Users.SaveChanges();
                }
                else return Json(false);
            //}
            return Json(true); 
        }

   
        [Route("api/AddEmployee")]
        public async Task<JsonResult> AddEmployee(bool flagMemory)
        {
            await GetData();//запрос данных
            if (Data.Count() == 0)//если бд  пустая сначала заполним ее и проинциц.
            {
                 var y= await Init(flagMemory);
                return Json(y);
            }
            return Json(Data);
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

        

    }
}
