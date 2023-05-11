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
using WpfApp1.Models.DBModels;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для LookRosters.xaml
    /// </summary>
    public partial class LookRosters : Window
    {
        private int sel;
        public LookRosters()
        {
            InitializeComponent();
        }
        public LookRosters(int id)
        {
            InitializeComponent();
            FillRosters(id);
        }
        public void FillRosters(int idd)
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@per", idd);
                cmd.CommandText = "SELECT * from PLAYERS where teamID=@per";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("TOURNAMENT");
                sda.Fill(dt);
                table.ItemsSource = dt.DefaultView;
            }
        }
    }
}
