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
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
       
        public ProfileWindow()
        {
            InitializeComponent();
            FillTextBoxesFromUserTable(FullName, Gender, DateOfBirth, IdNumber, Country, PhoneNumber, Email);
        }

        public static void FillTextBoxesFromUserTable(TextBox fullNameTextBox, TextBox genderTextBox, TextBox dateOfBirthTextBox, TextBox idNumberTextBox, TextBox countryTextBox, TextBox phoneNumberTextBox, TextBox emailTextBox)
        {
            try
            {
                using (var context = DBEntities.GetContext())
                {
                   
                    var user = context.User.FirstOrDefault();

                    if (user != null)
                    {
                        
                        fullNameTextBox.Text = user.FIO;
                        genderTextBox.Text = Convert.ToString(user.IdGender);
                        dateOfBirthTextBox.Text = DateTime.Now.ToString();
                        idNumberTextBox.Text = Convert.ToString( user.IdUser);
                        countryTextBox.Text = "Russia";
                        phoneNumberTextBox.Text = user.Phone;
                        emailTextBox.Text = user.Email;
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при заполнении текстбоксов: {ex.Message}");
            }
        }
    }
}
