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
    /// Логика взаимодействия для ball.xaml
    /// </summary>
    public partial class ball : Window
    {
        public ball()
        {
            InitializeComponent();
            try
            {
                using (var webClient = new MyWebClient())
                {
                    var pars = new NameValueCollection();
                    pars.Add("getHomework", Data.Login);
                    var response = webClient.UploadValues("http://localhost:3000", pars);
                    string str = System.Text.Encoding.UTF8.GetString(response);

                    if (str != "")
                    {
                        str.Split('\n').ToList().ForEach(item => CourseBlock.Items.Add(item));
                    }
                    else
                    {
                        CourseBlock.Items.Add("Ви ще не здали жодне домашнє завдання.");
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
