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
    /// Логика взаимодействия для AutorisationWindow.xaml
    /// </summary>
    public partial class AutorisationWindow : Window
    {

        
        public AutorisationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = DBEntities.GetContext()
                    .User.FirstOrDefault(u => u.Login == LoginTb.Text);

                if (user == null)
                {
                    MBClass.ErrorMB("Введен не верный логин");
                    LoginTb.Focus();
                    return;
                }
                if (user.Password != PasswordPb.Password)
                {
                    MBClass.ErrorMB("Введен не верный пароль");
                    PasswordPb.Focus();
                    return;
                }
                else
                {
                    switch (user.IdRole)
                    {
                        case 7:
                            new EventList().Show();
                            break;
                        case 8:
                            new ModerWindow().Show();
                            break;
                        case 3:
                            
                            break;
                        case 4:


                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new RegestrationWindow().Show();
            Close();
        }
    }
}
