using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LessonLivel2.Data.Memory;
using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using System.Collections.ObjectModel;

namespace LessonLivel2.Data
{
    internal class Client: IClient
    {
       public  HttpClient client = new HttpClient();
        public Client()
        {
            string path = $"{Environment.CurrentDirectory}/appsettings.json";
            if (File.Exists(path))
            {
                var template = new { Urls = String.Empty };
                var json = File.ReadAllText(path);
                var myObject = JsonConvert.DeserializeAnonymousType(json, template);
                var t = myObject.Urls.Split(';')[0];
                client = new HttpClient
                {
                    BaseAddress = new Uri(t),
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public async Task<bool> Add(Employee employee)
        {
          
            try
            {
                var SeriaType = JsonConvert.SerializeObject(employee);
                var response = await client.PostAsJsonAsync("/api/Add", SeriaType);
                if (response.IsSuccessStatusCode)
                {
                    //var products1 = await response.Content.ReadAsStringAsync();
                    //var tg = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(products1);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public Task<bool> AddDep(Department dep)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Employee>> AddEmployee()
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Employee>> AddEmployee(bool flag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteData()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Employee employee)
        {
            ObservableCollection<Employee> products = new();
            try
            {
                var response = await client.GetAsync("/api/GetData");
                if (response.IsSuccessStatusCode)
                {
                    var products1 = await response.Content.ReadAsStringAsync();
                    var tg = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(products1);
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }

        public async Task<ObservableCollection<Employee>> GetData()
        {
            ObservableCollection<Employee> products = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync("/api/GetData");
                if (response.IsSuccessStatusCode)
                {
                   var products1 = await response.Content.ReadAsStringAsync();
                    var tg= JsonConvert.DeserializeObject<ObservableCollection<Employee>>(products1);
                    return tg;
                }
            }
            catch (Exception)
            {
            }
            return products;
        }



    }
}
