using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : Page
    {
        public Table()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Logo,[Team Name],W,T,L,P FROM TOURNAMENT ORDER BY P DESC";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("TOURNAMENT");
                sda.Fill(dt);
                table.ItemsSource = dt.DefaultView;
            }
        }
    }
}
