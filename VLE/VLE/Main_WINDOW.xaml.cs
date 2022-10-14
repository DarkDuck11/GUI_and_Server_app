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
    /// Логика взаимодействия для Main_WINDOW.xaml
    /// </summary>
    public partial class Main_WINDOW : Window
    {
        public Main_WINDOW()
        {
            InitializeComponent();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            search ser = new search();
            ser.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            homework1 home = new homework1();
            home.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ball the_ball = new ball();
            the_ball.Show();
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult logout = MessageBox.Show("Ви впевнені, що хочете вийти?", "Повідомлення", MessageBoxButton.YesNo);
            if (logout == MessageBoxResult.Yes)
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("logout", "");
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);

                        if (str == "true")
                        {
                            log back = new log();
                            back.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Сталася помилка, вийти не вдалось.");
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
