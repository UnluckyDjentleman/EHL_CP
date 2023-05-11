using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models.Repos;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddingPlayer.xaml
    /// </summary>
    public partial class AddingPlayer : Window
    {
        private UnitOfWork context;
        public int teamid;
        public AddingPlayer()
        {
            InitializeComponent();
            context = new UnitOfWork();
        }
        public AddingPlayer(int id)
        {
            InitializeComponent();
            teamid = id;
            context = new UnitOfWork();
        }
    }
}