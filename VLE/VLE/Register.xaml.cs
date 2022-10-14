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
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Specialized;

namespace VLE
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length == 0 || Password.Password.Length == 0 || FirstName.Text.Length == 0 || LastName.Text.Length == 0 || Role.Text.Length == 0)
            {
                MessageBox.Show("Заповніть всі поля.");
            }
            else if (Login.Text.Length < 6)
            {
                MessageBox.Show("Логін повинен містити мінімум 6 символів.");
            }
            else if (!Regex.IsMatch(Login.Text, @"^[0-9a-zA-Z]+$"))
            {
                MessageBox.Show("Логін повинен містити тільки цифри та латинські букви.");
            }
            else if (Password.Password.Length < 6)
            {
                MessageBox.Show("Пароль повинен містити мінімум 6 символів!");
            }
            else if (!Regex.IsMatch(Password.Password, @"^[0-9a-zA-Z]+$"))
            {
                MessageBox.Show("Пароль повинен містити тільки цифри та латинські букви!");
            }
            else if(Role.Text.ToLower() != "teacher" && Role.Text.ToLower() != "student")
            {
                MessageBox.Show("Некоректна роль.");
            }
            else
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("registration", Login.Text + "&" + Password.Password + '&' + FirstName.Text + '&' + LastName.Text + '&' + Role.Text);
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);
                        if (str != "Користувач з даним логіном вже існує.")
                        {
                            MessageBox.Show(str);
                            log back = new log();
                            back.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(str);
                        }
                    }
                }
                catch (WebException)
                {
                    MessageBox.Show("Сервер не відповідає, спробуйте пізніше.");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            log back = new log();
            back.Show();
            Close();
        }
    }
}
