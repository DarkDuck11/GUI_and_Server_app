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
using System.Text.RegularExpressions;

namespace VLE
{
    /// <summary>
    /// Логика взаимодействия для homework_student.xaml
    /// </summary>
    public partial class homework_student : Window
    {
        string userName;
        string courseName;
        public homework_student(string user, string course, string description)
        {
            userName = user;
            courseName = course;
            InitializeComponent();
            text.Text = description;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Rating.Text.Length == 0)
            {
                MessageBox.Show("Заповніть поле.");
            }
            else if (!Regex.IsMatch(Rating.Text, @"^[0-9a-zA-Z]+$"))
            {
                MessageBox.Show("Некоректна оцінка.");
            }
            else if (Convert.ToInt32(Rating.Text) < 0 || Convert.ToInt32(Rating.Text) > 5)
            {
                MessageBox.Show("Оцінка повинна бути в межах від 0 до 5.");
            }
            else
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("addRating", userName + '&' + courseName + '&' + Rating.Text);
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);

                        if (str == "Завдання оцінено.")
                        {
                            MessageBox.Show(str);
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
