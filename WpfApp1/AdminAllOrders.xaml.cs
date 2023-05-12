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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminAllOrders.xaml
    /// </summary>
    public partial class AdminAllOrders : Window
    {
        public AdminAllOrders()
        {
            InitializeComponent();
            FillDataGrid();
        }
        public void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select orders.userid,USERS.NAME,USERS.SURNAME,USERS.TELEPHONE,BONUS.id as bonusID,BONUS.header from USERS,BONUS,orders where orders.id=bonus.id and orders.userid=USERS.id";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("OrdersAll");
                sda.Fill(dt);
                table.ItemsSource = dt.DefaultView;
            }
        }
    }
}
