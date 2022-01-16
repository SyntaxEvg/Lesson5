using LessonLivel2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    internal class ListEMP
    {
        public IEnumerable<Employee> GetEnumerator()
        {
            var TestData = new ObservableCollection<Employee>();
            TestData.Add(new Employee("Панкратова", "Мира", "Павловна", 23, new Department("МВД")));
            TestData.Add(new Employee("Антипова", "София", "Егоровна", 27, new Department("Минтруд")));
            TestData.Add(new Employee("Морозов", "Дмитрий", "Иванович", 33, new Department("МЧС")));
            TestData.Add(new Employee("Семенова", "Мария", "Ивановна", 28, new Department("Минтруд")));
            TestData.Add(new Employee("Лазарев", "Александр", "Арсентьевич", 29, new Department("МВД")));
            TestData.Add(new Employee("Власов", "Владимир", "Александрович", 47, new Department("Минтруд")));
            TestData.Add(new Employee("Копылова", "Александра", "Анатольевна", 41, new Department("Минфин")));
            TestData.Add(new Employee("Новиков", "Дамир", "Ярославович", 35, new Department("МИД")));
            TestData.Add(new Employee("Гуров", "Данила", "Платонович", 37, new Department("Минтруд")));
            TestData.Add(new Employee("Меркулов", "Максим", "Артёмович", 25, new Department("Минфин")));

            foreach (var item in TestData)
            {
                yield return item;
            }


        }




    }
}
