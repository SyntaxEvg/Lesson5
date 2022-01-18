using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;
using LessonLivel2.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LessonLivel2.SaveConfig;
using LessonLivel2.Data.sql;

namespace LessonLivel2
{
    public partial class MainWindow : Window
    {
        
        public  MainWindow()
        {          
            InitializeComponent();
        }
        private  async void Window_Closed(object sender, EventArgs e)
        {
            //тут проверка и сохранение обьекта 
            using (var db = new UserContext())
            {
               await  db.SaveChangesAsync();
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(var db=new UserContext())
            {
               await db.SaveChangesAsync();
            }
        }

       
    }
}
