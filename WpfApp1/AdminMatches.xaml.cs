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
using WpfApp1.Models.Repos;
using WpfApp1.Models.Records;
using WpfApp1.ViewModels;
using WpfApp1.Models.DBModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminMatches.xaml
    /// </summary>
    public partial class AdminMatches : Page
    {
        private UnitOfWork context;
        public AdminMatches()
        {
            InitializeComponent();
            context = new UnitOfWork();
            this.DataContext = new MatchVM();
        }

        private void updater_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
