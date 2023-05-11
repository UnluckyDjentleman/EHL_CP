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
using WpfApp1.Models.DBModels;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Window
    {
        private UnitOfWork unitOfWork;
        public USERS userSignedIn;
        public MainFrame()
        {
            InitializeComponent();
            Main.Content = new Table();
        }
        public MainFrame(USERS user)
        {
            InitializeComponent();
            userSignedIn = user;
            Main.Content = new Table();
        }
        private void Table_Click(object sender, RoutedEventArgs e) { 
            Main.Content = new Table(); 
        }

        private void Matches_Click(object sender, RoutedEventArgs e) {
            if (userSignedIn.ROLE == 1)
            {
                Main.Content = new AdminMatches();
            }
            else
            {
                Main.Content = new Matches();
            }
        }
        private void Roster_Click(object sender, RoutedEventArgs e) {
            if (userSignedIn.ROLE == 1)
            {
                Main.Content = new AdminRosters();
            }
            else
            {
                Main.Content = new Rosters();
            }
        }
        private void Article_Click(object sender, RoutedEventArgs e) {
            if (userSignedIn.ROLE == 1)
            {
                Main.Content = new AdminArticles();
            }
            else
            {
                Main.Content = new Article();
            }
        }
        private void Bonus_Click(object sender, RoutedEventArgs e) {
            if (userSignedIn.ROLE == 1)
            {
                Main.Content = new AdminBonus();
            }
            else
            {
                Main.Content = new Bonus(userSignedIn);
            }
        }
    }
}
