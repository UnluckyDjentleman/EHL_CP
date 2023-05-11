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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Matches.xaml
    /// </summary>
    public partial class Matches : Page
    {
        public List<MATCHES> matches { get; set; }
        public Matches()
        {
            InitializeComponent();
            FillData();
        }
        public void FillData()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Logo1,Goals1,Goals2,Logo2,GAME_DATE FROM Matches order by GAME_DATE";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Matches");
                sda.Fill(dt);
                MatchList.ItemsSource = dt.DefaultView;
            }
        }
    }
}
