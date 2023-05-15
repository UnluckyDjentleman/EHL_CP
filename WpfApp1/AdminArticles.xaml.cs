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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminArticles.xaml
    /// </summary>
    public partial class AdminArticles : Page
    {
        public AdminArticles()
        {
            InitializeComponent();
            DataContext = new ArticleVM();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainFrame))
                {
                    var wind = new MainWindow();
                    wind.frameAuth.Content = new Login();
                    wind.Show();
                    window.Close();
                }
            }
        }
    }
}
