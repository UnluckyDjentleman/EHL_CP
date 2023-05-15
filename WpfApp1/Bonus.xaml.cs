using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using WpfApp1.Models.DBModels;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Command;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Bonus.xaml
    /// </summary>
    public partial class Bonus : Page
    {
        public UnitOfWork unitOfWork;
        public USERS user;
        public bonus bonuS;
        public Bonus(USERS userSignedIn)
        {
            InitializeComponent();
            DataContext = new BonusVM();
            unitOfWork = new UnitOfWork();
            user = userSignedIn;
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
