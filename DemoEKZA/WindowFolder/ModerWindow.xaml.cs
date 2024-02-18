using DemoEKZA.DataFolder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Логика взаимодействия для ModerWindow.xaml
    /// </summary>
    public partial class ModerWindow : Window
    {
        public ModerWindow()
        {
            InitializeComponent();
            DgUser.ItemsSource = DBEntities.GetContext().User
                    .ToList().OrderBy(u => u.FIO);
            SurnameTB.ItemsSource = DBEntities.GetContext()
               .User.ToList();
            EventCb.ItemsSource = DBEntities.GetContext()
               .Event.ToList();

            UpdateRowCountLabel();
        }
        private void UpdateRowCountLabel()
        {
            
            ColvoLabel.Content = "Количество: " + DgUser.Items.Count;
        }
        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var context = DBEntities.GetContext())
            {
                
                string selectedSurname = (SurnameTB.SelectedItem as User)?.FIO;
                string selectedEvent = (EventCb.SelectedItem as Event)?.EventName;

                
                var query = context.User.AsQueryable();

                
                if (!string.IsNullOrEmpty(selectedSurname))
                    query = query.Where(user => user.FIO == selectedSurname);

                
                int? selectedEventId = context.Event.FirstOrDefault(ev => ev.EventName == selectedEvent)?.IdEvent;

                
                if (selectedEventId != null)
                {
                    query = query.Where(user => user.IdEvent == selectedEventId);
                }

                var filteredUsers = query.ToList();

                
                DgUser.ItemsSource = filteredUsers;
            }
        }
    }
}
