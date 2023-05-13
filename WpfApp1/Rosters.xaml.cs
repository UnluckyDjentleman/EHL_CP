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
    /// Логика взаимодействия для Rosters.xaml
    /// </summary>
    public partial class Rosters : Page
    {
        private EHLModel model;
        public Rosters()
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
                var title=model.TEAMS.Where(n => n.teamid == ListViewTeams.SelectedIndex + 1).Single();
                window.Title = title.Team_Name;
                window.Icon = BitmapFrame.Create(Application.GetResourceStream(new Uri(title.Logo, UriKind.RelativeOrAbsolute)).Stream);
                window.Show();
            }
        }
    }
}
