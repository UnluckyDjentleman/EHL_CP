﻿using System;
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
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AdminRosters.xaml
    /// </summary>
    public partial class AdminRosters : Page
    {
        private EHLModel model;
        public AdminRosters()
        {
            InitializeComponent();
            model = new EHLModel();
            FillExpanders();
        }
        public void FillExpanders()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT TEAMS.teamid, TEAMS.Logo, TEAMS.[Team Name] from TEAMS";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("EXPANDERA");
                sda.Fill(dt);
                ListViewTeams.ItemsSource = dt.DefaultView;
            }
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                int i = ListViewTeams.SelectedIndex + 1;
                var window = new AdminLookRosters(i);
                var title = model.TEAMS.Where(n => n.teamid == ListViewTeams.SelectedIndex + 1).Single();
                window.Title = title.Team_Name;
                window.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri(title.Logo, UriKind.RelativeOrAbsolute)).Stream);
                window.Show();
            }
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
