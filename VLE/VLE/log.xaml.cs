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
using System.Net;
using System.Collections.Specialized;

namespace VLE
{
    /// <summary>
    /// Логика взаимодействия для log.xaml
    /// </summary>
    public partial class log : Window
    {
        public log()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(Login.Text.Length == 0 || Password.Password.Length == 0)
            {
                MessageBox.Show("Заповніть всі поля.");
            }
            else
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("login", Login.Text + "&" + Password.Password);
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);

                        if (str == "Teacher")
                        {
                            Data.Login = Login.Text;
                            main_window_teachet maiin = new main_window_teachet();
                            maiin.Show();
                            Close();
                        }
                        else if(str == "Student")
                        {
                            Data.Login = Login.Text;
                            Main_WINDOW main_WINDOW = new Main_WINDOW();
                            main_WINDOW.Show();
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
    }
}
