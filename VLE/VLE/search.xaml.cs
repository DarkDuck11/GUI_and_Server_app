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
    /// Логика взаимодействия для search.xaml
    /// </summary>
    public partial class search : Window
    {
        public search()
        {
            InitializeComponent();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length == 0)
            {
                MessageBox.Show("Заповніть полe.");
            }
            else
            {
                try
                {
                    using (var webClient = new MyWebClient())
                    {
                        var pars = new NameValueCollection();
                        pars.Add("searchCourse", Name.Text);
                        var response = webClient.UploadValues("http://localhost:3000", pars);
                        string str = System.Text.Encoding.UTF8.GetString(response);

                        if (str != "Курс не знайдено.")
                        {
                            course1 curs = new course1(str);
                            curs.Show();
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
