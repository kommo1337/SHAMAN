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
    /// Логика взаимодействия для EventList.xaml
    /// </summary>
    public partial class EventList : Window
    {
        public EventList()
        {
            InitializeComponent();
            DgEvent.ItemsSource = DBEntities.GetContext().Event
                .ToList().OrderBy(u => u.EventName);
            EventCb.ItemsSource = DBEntities.GetContext()
                .Event.ToList();
            NapCB.ItemsSource = DBEntities.GetContext()
                .Napravlenie.ToList();


            PrivetTB.Text = GetGreeting();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedEvent = (Event)EventCb.SelectedItem;
            var selectedNapravlenie = (Napravlenie)NapCB.SelectedItem;

            if (selectedEvent != null && selectedNapravlenie != null)
            {
                var result = DBEntities.GetContext()
                    .Event.ToList()
                    .Where(x => x.IdEvent == selectedEvent.IdEvent)
                    .ToList();

                DgEvent.ItemsSource = result;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AddModer().Show();
            Close();
        }
    }
}
