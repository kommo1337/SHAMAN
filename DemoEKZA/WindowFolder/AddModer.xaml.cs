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
    /// Логика взаимодействия для AddModer.xaml
    /// </summary>
    public partial class AddModer : Window
    {
        public AddModer()
        {
            InitializeComponent();
            MeroTb.ItemsSource = DBEntities.GetContext()
                .Event.ToList();
            NapaCb.ItemsSource = DBEntities.GetContext()
                .Napravlenie.ToList();
            RoleCb.ItemsSource = DBEntities.GetContext()
                .Role.ToList();
            GenderCb.ItemsSource = DBEntities.GetContext()
                .Gender.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordTB.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordTB.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Выберите роль для пользователя");
                RoleCb.Focus();
            }
            else
            {
                try
                {
                    int index = RoleCb.SelectedIndex + 1;
                    int index2 = GenderCb.SelectedIndex + 1;
                    int index3 = NapaCb.SelectedIndex + 1;
                    int index4 = MeroTb.SelectedIndex + 1;
                    DBEntities.GetContext().User.Add(new User()
                    {
                        Login = LoginTb.Text,
                        Password = PasswordTB.Password,
                        IdRole = index,
                        IdGender = index2,
                        IdNapravlenie = index3,
                        IdEvent = index4,
                        FIO = FIOTb.Text,
                        Phone = PhonTb.Text,
                        Email = EmailTb.Text
                    });
                    DBEntities.GetContext().SaveChanges();
                    MBClass.InfoMB("Пользователь успешно добавлен");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                    throw;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new EventList().Show();
            Close();
        }
    }

}

