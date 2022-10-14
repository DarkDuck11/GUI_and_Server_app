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
    /// Логика взаимодействия для homework_ball.xaml
    /// </summary>
    public partial class homework_ball : Window
    {
        public homework_ball()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (user.Text.Length == 0 || course.Text.Length == 0)
            {
                MessageBox.Show("Заповніть поле.");
            }
            else
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("searchHomework", user.Text + '&' + course.Text);
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);

                        if (str.Split('&')[0] == "true")
                        {
                            homework_student mor = new homework_student(user.Text, course.Text, str.Split('&')[1]);
                            mor.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show(str.Split('&')[1]);
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
