using DemoEKZA.ClassFolder;
using DemoEKZA.DataFolder;
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
using System.Windows.Shapes;

namespace DemoEKZA.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для OrganisatorWindow.xaml
    /// </summary>
    public partial class OrganisatorWindow : Window
    {
        public OrganisatorWindow()
        {
            InitializeComponent();
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            new EventList().Show();
            Close();
        }

        private void UchastnikiBTN_Click(object sender, RoutedEventArgs e)
        {
            // сюда открыть юзеров
        }

        private void ZhirnieBTN_Click(object sender, RoutedEventArgs e)
        {
            // сюда открыть жюри
        }

        private void ProfileBTN_Click(object sender, RoutedEventArgs e)
        {
            new ProfileWindow().Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string time = GetGreeting();
            string gender = DBEntities.GetContext().User.Where(x => x.IdUser == MBClass.userid).First().Gender.IdGender.ToString();
            if(gender == "1")
            {
                gender = "Mr";
            }
            else
            {
                gender = "Mrs";
            }
            string name = DBEntities.GetContext().User.Where(x => x.IdUser == MBClass.userid).First().FIO;
            privet.Text = time + "\n" + gender + " " + name;
            
        }

        private string GetGreeting()
        {
            int currentHour = DateTime.Now.Hour;

            if (currentHour >= 9 && currentHour <= 11)
            {
                return "Доброе утро!";
            }
            else if (currentHour > 11 && currentHour <= 18)
            {
                return "Добрый день!";
            }
            else
            {
                return "Добрый вечер!";
            }
        }
    }
}
