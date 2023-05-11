using System;
using System.Collections.Generic;
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
using WpfApp1.Models.Records;
using WpfApp1.Models.Repos;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UpdatingMatch.xaml
    /// </summary>
    public partial class UpdatingMatch : Window
    {
        private UnitOfWork context;
        private EHLModel ehl;
        public int matchID;
        public UpdatingMatch()
        {
            InitializeComponent();
        }
        public UpdatingMatch(int id)
        {
            InitializeComponent();
            ehl = new EHLModel();
            matchID = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var matchidd = new SqlParameter("@match", matchID);
                var hg = new SqlParameter("@hg", Convert.ToInt32(home.Text));
                var gg = new SqlParameter("@gg", Convert.ToInt32(guest.Text));
                if (Convert.ToInt32(home.Text) > Convert.ToInt32(guest.Text))
                {
                    ehl.Database.ExecuteSqlCommand("Update MATCHES set Goals1=@hg,Goals2=@gg where matchid=@match", hg, gg, matchidd);
                    ehl.Database.ExecuteSqlCommand("Update TOURNAMENT set W=W+1, P=P+2 from MATCHES where (TOURNAMENT.teamid=MATCHES.Team1 and MATCHES.matchid=@match)" +
                        "Update TOURNAMENT set L = L + 1 from MATCHES where (TOURNAMENT.teamid = MATCHES.Team2 and MATCHES.matchid = @match)", matchidd);
                    ehl.SaveChanges();
                }
                else if (Convert.ToInt32(home.Text) < Convert.ToInt32(guest.Text))
                {
                    ehl.Database.ExecuteSqlCommand("Update MATCHES set Goals1=@hg,Goals2=@gg where matchid=@match", hg, gg, matchidd);
                    ehl.Database.ExecuteSqlCommand("Update TOURNAMENT set W=W+1, P=P+2 from MATCHES where (TOURNAMENT.teamid=MATCHES.Team2 and MATCHES.matchid=@match)" +
                        "Update TOURNAMENT set L = L + 1 from MATCHES where (TOURNAMENT.teamid = MATCHES.Team1 and MATCHES.matchid = @match)", matchidd);
                    ehl.SaveChanges();
                }
                else
                {
                    ehl.Database.ExecuteSqlCommand("Update MATCHES set Goals1=@hg,Goals2=@gg where matchid=@match", hg, gg, matchidd);
                    ehl.Database.ExecuteSqlCommand("Update TOURNAMENT set T=T+1, P=P+1 from MATCHES where (TOURNAMENT.teamid=MATCHES.Team1 and MATCHES.matchid=@match)" +
                        "Update TOURNAMENT set T = T + 1,P=P+1 from MATCHES where (TOURNAMENT.teamid = MATCHES.Team2 and MATCHES.matchid = @match)", matchidd);
                    ehl.SaveChanges();
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при обновлении данных");
            }
        }
    }
}
