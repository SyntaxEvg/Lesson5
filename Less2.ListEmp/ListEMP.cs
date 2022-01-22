using LessonLivel2.ModelData;
using LessonLivel2.ModelData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLivel2.Data
{
    public class ListEMP
    {
        bool FlagMemory=false;
        public ListEMP(bool flagMemory)
        {
            FlagMemory=flagMemory;
        }


        public IEnumerable<object> GetEnumerator()
        {
            Random rand = new Random();
            var TestData = new ObservableCollection<Employee>();
            if (FlagMemory)
            {
                TestData.Add(new Employee { Surname = "Панкратова", Name = "Мира", Patranomic = "Павловна", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Антипова", Name = "София", Patranomic = "Егоровна", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Морозов", Name = "Дмитрий", Patranomic = "Иванович", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Семенова", Name = "Мария", Patranomic = "Ивановна", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Лазарев", Name = "Александр", Patranomic = "Арсентьевич", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Власов", Name = "Владимир", Patranomic = "Александрович", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Копылова", Name = "Александра", Patranomic = "Анатольевна", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Новиков", Name = "Дамир", Patranomic = "Ярославович", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Гуров", Name = "Данила", Patranomic = "Платонович", Age = rand.Next(10, 89) });
                TestData.Add(new Employee { Surname = "Меркулов", Name = "Максим", Patranomic = "Артёмович", Age = rand.Next(10, 89) });

                foreach (var item in TestData)
                {
                    yield return item;
                }
                
            }
            else
            {
                List<object> list = new List<object>();
                var DepartmentList = new List<ModelData.Model.Department>()
            {
                 new Department { DepartName ="МВД" },
                 new Department { DepartName ="Минтруд"},
                 new Department { DepartName ="МЧС"},
                 new Department { DepartName ="МИД"},
                 new Department { DepartName ="Минфин"},

            };

               
                var USERList = new List<Employee>()
            {
                 new Employee{Surname ="Панкратова",Name= "Мира",Patranomic= "Павловна",Age= rand.Next(10,89) },
                 new Employee{Surname ="Антипова",Name= "София",Patranomic= "Егоровна", Age=rand.Next(10,89)        },
                 new Employee{Surname ="Морозов",Name= "Дмитрий",Patranomic= "Иванович",Age= rand.Next(10,89)       },
                 new Employee{Surname ="Семенова",Name= "Мария",Patranomic= "Ивановна",Age= rand.Next(10,89)        },
                 new Employee{Surname ="Лазарев",Name= "Александр",Patranomic= "Арсентьевич",Age= rand.Next(10,89)  },
                 new Employee{Surname ="Власов",Name= "Владимир",Patranomic= "Александрович",Age= rand.Next(10,89)  },
                 new Employee{Surname ="Копылова",Name= "Александра",Patranomic= "Анатольевна", Age=rand.Next(10,89)},
                 new Employee{Surname ="Новиков",Name= "Дамир",Patranomic= "Ярославович", Age=rand.Next(10,89) },
                 new Employee{Surname ="Гуров",Name= "Данила", Patranomic="Платонович",Age= rand.Next(10,89) },
                 new Employee{Surname ="Меркулов",Name= "Максим",Patranomic=  "Артёмович",Age= rand.Next(10,89) },
                 new Employee{Surname="Новиков",Name="Данила",Patranomic="Никитич",Age=rand.Next(10,89)},
                 new Employee{Surname="Егоров",Name="Никита",Patranomic="Андреевич",Age=rand.Next(10,89)},
                 new Employee{Surname="Устинова",Name="Алиса",Patranomic="Артёмовна",Age=rand.Next(10,89)},
                 new Employee{Surname="Дмитриев",Name="Илья",Patranomic="Даниилович",Age=rand.Next(10,89)},
                 new Employee{Surname="Давыдов",Name="Павел",Patranomic="Павлович",Age=rand.Next(10,89)},
                 new Employee{Surname="Бондарева",Name="Мелания",Patranomic="Николаевна",Age=rand.Next(10,89)},
                 new Employee{Surname="Волкова",Name="Полина",Patranomic="Кирилловна",Age=rand.Next(10,89)},
                 new Employee{Surname="Горшков",Name="Арсений",Patranomic="Олегович",Age=rand.Next(10,89)},
                 new Employee{Surname="Марков",Name="Артём",Patranomic="Иванович",Age=rand.Next(10,89)},
                 new Employee{Surname="Зубков",Name="Артём",Patranomic="Артёмович",Age=rand.Next(10,89)},
                 new Employee{Surname="Чернов",Name="Пётр",Patranomic="Святославович",Age=rand.Next(10,89)},
                 new Employee{Surname="Поляков",Name="Дмитрий",Patranomic="Лукич",Age=rand.Next(10,89)},
                 new Employee{Surname="Зубова",Name="Мария",Patranomic="Игоревна",Age=rand.Next(10,89)},
                 new Employee{Surname="Медведева",Name="Вера",Patranomic="Тимуровна",Age=rand.Next(10,89)},
                 new Employee{Surname="Савицкий",Name="Мирон",Patranomic="Олегович",Age=rand.Next(10,89)},
                 new Employee{Surname="Котов",Name="Артём",Patranomic="Львович",Age=rand.Next(10,89)},
                 new Employee{Surname="Антипова",Name="София",Patranomic="Егоровна",Age=rand.Next(10,89)},
                 new Employee{Surname="Дьяков",Name="Макар",Patranomic="Александрович",Age=rand.Next(10,89)},
                 new Employee{Surname="Васильева",Name="Алиса",Patranomic="Ильинична",Age=rand.Next(10,89)},
                 new Employee{Surname="Олейников",Name="Максим",Patranomic="Платонович",Age=rand.Next(10,89)},
                 new Employee{Surname="Климова",Name="Милана",Patranomic="Львовна",Age=rand.Next(10,89)},
                 new Employee{Surname="Иванов",Name="Кирилл",Patranomic="Даниилович",Age=rand.Next(10,89)},
                 new Employee{Surname="Лебедев",Name="Алексей",Patranomic="Максимович",Age=rand.Next(10,89)},
                 new Employee{Surname="Плотникова",Name="Софья",Patranomic="Сергеевна",Age=rand.Next(10,89)},
                 new Employee{Surname="Смирнова",Name="Елизавета",Patranomic="Макаровна",Age=rand.Next(10,89)},
                 new Employee{Surname="Дорофеева",Name="Александра",Patranomic="Егоровна",Age=rand.Next(10,89)},
                 new Employee{Surname="Медведева",Name="Арина",Patranomic="Евгеньевна",Age=rand.Next(10,89)},
                 new Employee{Surname="Орлов",Name="Иван",Patranomic="Михайлович",Age=rand.Next(10,89)},
                 new Employee{Surname="Гришина",Name="Анастасия",Patranomic="Викторовна",Age=rand.Next(10,89)},
                 new Employee{Surname="Шмелев",Name="Матвей",Patranomic="Всеволодович",Age=rand.Next(10,89)},
                 new Employee{Surname="Соловьева",Name="Вера",Patranomic="Вячеславовна",Age=rand.Next(10,89)},
                 new Employee{Surname="Уткин",Name="Константин",Patranomic="Егорович",Age=rand.Next(10,89)},
                 new Employee{Surname="Галкина",Name="Злата",Patranomic="Данииловна",Age=rand.Next(10,89)},
                 new Employee{Surname="Федоров",Name="Иван",Patranomic="Кириллович",Age=rand.Next(10,89)},
                 new Employee{Surname="Ермаков",Name="Савелий",Patranomic="Павлович",Age=rand.Next(10,89)},
                 new Employee{Surname="Ермолаев",Name="Александр",Patranomic="Тимофеевич",Age=rand.Next(10,89)},
                 new Employee{Surname="Павлова",Name="Злата",Patranomic="Савельевна",Age=rand.Next(10,89)},
                 new Employee{Surname="Фирсова",Name="Виктория",Patranomic="Максимовна",Age=rand.Next(10,89)},
                 new Employee{Surname="Фролов",Name="Олег",Patranomic="Дмитриевич",Age=rand.Next(10,89)},
                 new Employee{Surname="Литвинова",Name="Яна",Patranomic="Егоровна",Age=rand.Next(10,89)},
                 new Employee{Surname="Филиппова",Name="Дарья",Patranomic="Андреевна",Age=rand.Next(10,89)},

            };
                list.Add(DepartmentList);
                list.Add(USERList);
                foreach (var item in list)
                {
                    yield return item;
                }
            }

           

            


        }




    }
}
