using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models.Repos;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private UnitOfWork context;
        public Registration()
        {
            InitializeComponent();
            context = new UnitOfWork();
            FillCombobox();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("./Login.xaml", UriKind.Relative));
        }

        private void MoveToMainFrame_Click(object sender, RoutedEventArgs e)
        {
            var regex = new Regex(@"^((\+375)((29)||(33)||(44))\d{7})$");
            if (!regex.IsMatch(telephonebox.Text))
                MessageBox.Show("Введите корректный номер телефона в формате +375XXXXXXXXX.", "Ошибка!", MessageBoxButton.OK);
            else if (string.IsNullOrEmpty(namebox.Text) || string.IsNullOrEmpty(surnamebox.Text) ||
                string.IsNullOrEmpty(SecureStringToString(passwordbox.SecurePassword)) || string.IsNullOrEmpty(favteam.Text))
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка!", MessageBoxButton.OK);
            else
            {
                try
                {
                    var result = context.UsersRepo.AddNewUser(Guid.NewGuid(), namebox.Text, surnamebox.Text,
                        SecureStringToString(passwordbox.SecurePassword), telephonebox.Text, favteam.SelectedIndex + 1);
                    if (result)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
                        NavigationService.Navigate((new Uri("./Login.xaml", UriKind.Relative)));
                    }
                    else throw new Exception();
                }
                catch
                {
                    MessageBox.Show("Already exist", "Error!", MessageBoxButton.OK);
                }
            }
        }
        private void FillCombobox()
        {
            string ConString = ConfigurationManager.ConnectionStrings["EHLModel"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT teamid,[Team Name] from TEAMS";
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("TOURNAMENT");
                sda.Fill(dt);
                favteam.ItemsSource = dt.DefaultView;
                favteam.DisplayMemberPath = "[Team Name]";
            }
        }
        private static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
