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
using System.Windows.Shapes;
using WpfApp1.Models.Records;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminLookRosters.xaml
    /// </summary>
    public partial class AdminLookRosters : Window
    {
        public AdminLookRosters(int id)
        {
            InitializeComponent();
            PlayerVM matchVM = new PlayerVM();
            DataContext = matchVM;
            matchVM.GetTID = id;
            var rr = matchVM.EHLRecord.PlayersRecords.Where(x=>x.teamID==id);
            table.ItemsSource = rr;
        }
    }
}
