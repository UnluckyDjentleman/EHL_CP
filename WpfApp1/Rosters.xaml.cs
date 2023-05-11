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
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Rosters.xaml
    /// </summary>
    public partial class Rosters : Page
    {
        public Rosters()
        {
            InitializeComponent();
            FillExpanders();
        }
        public void FillExpanders()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Logo,[Team Name] FROM TOURNAMENT";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("TOURNAMENT");
                sda.Fill(dt);
                ListViewTeams.ItemsSource = dt.DefaultView;
            }
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                var window=new LookRosters(ListViewTeams.SelectedIndex+1);
                window.Show();
            }
        }
    }
}
