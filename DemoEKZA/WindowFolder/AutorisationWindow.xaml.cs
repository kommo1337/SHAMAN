using DemoEKZA.ClassFolder;
using DemoEKZA.DataFolder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private int _failedAttempts;
        private DateTime _lockoutEnd;
        public static int UserNumebr;

        public AutorisationWindow()
        {
            InitializeComponent();
            LoadCaptcha();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginTb.Text;
            string password = PasswordPb.Password;

            if (IsValidLogin(username, password))
            {
                _failedAttempts = 0;
            }
            else
            {
                _failedAttempts++;

                if (_failedAttempts >= 3)
                {
                    _lockoutEnd = DateTime.Now.AddSeconds(10);
                    MBClass.ErrorMB("Слишком много неудачных попыток входа. Система будет заблокирована.");
                    IsEnabled = false;

                    CheckLockoutStatusAsync();
                }
            }
        }

        public async Task CheckLockoutStatusAsync()
        {
            while (true)
            {
                if (DateTime.Now >= _lockoutEnd)
                {
                    IsEnabled = true;
                    break;
                }

                await Task.Delay(1000);
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            try
            {
                var user = DBEntities.GetContext()
                    .User.FirstOrDefault(u => u.Login == LoginTb.Text);
                string userInput = CaptchaTB.Text;
                

                if (VerifyCaptcha1(userInput) == false)
                {
                    return false;
                }

                if (user == null)
                {
                    MBClass.ErrorMB("Пользователь не введен");
                    return false;
                }
                    if (user == null)
                {
                    MBClass.ErrorMB("Введен не верный логин");
                    LoginTb.Focus();
                    return false;
                }
                if (user.Password != PasswordPb.Password)
                {
                    MBClass.ErrorMB("Введен не верный пароль");
                    PasswordPb.Focus();
                    return false;
                }
                else
                {
                    switch (user.IdRole)
                    {
                        case 7:
                            new EventList().Show();
                            UserNumebr = user.IdUser;
                            break;
                        case 8:
                            new ModerWindow().Show();
                            UserNumebr = user.IdUser;
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
            return true;
        }

        private void LoadCaptcha()
        {
            var captchaTuple = CaptchaGenerator.GenerateCaptcha(200, 50, 50);
            var captchaImage = captchaTuple.Item2;

            // Отображение изображения в Image элементе WPF
            CaptchaImage.Source = ConvertBitmapToBitmapImage(captchaImage);
        }

        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private bool VerifyCaptcha1(string userInput)
        {
            if (CaptchaGenerator.VerifyCaptcha(userInput))
            {
                MessageBox.Show("CAPTCHA введена правильно!");
                return true;
            }
            else
            {
                MessageBox.Show("CAPTCHA введена неправильно. Попробуйте еще раз.");
                LoadCaptcha();
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new RegestrationWindow().Show();
            Close();
        }
    }
}
