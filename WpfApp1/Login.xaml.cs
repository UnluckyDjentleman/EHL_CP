using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using WpfApp1.Models.Repos;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private UnitOfWork context;

        public Login()
        {
            InitializeComponent();
            context = new UnitOfWork();
        }
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("./Registration.xaml", UriKind.Relative));
        }
        private void MoveToMainFrame_Click(object sender, RoutedEventArgs e)
        {
            var name = namebox.Text;
            var surname = surnamebox.Text;
            var password = SecureStringToString(passwordbox.SecurePassword);
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите имя!", "Ошибка!", MessageBoxButton.OK);
            }
            else if (string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Введите фамилию!", "Ошибка!", MessageBoxButton.OK);
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK);
            }
            else
            {
                var user = context.UsersRepo.GetUserByNameAndSurname(name, surname);
                if (user == null)
                {
                    MessageBox.Show("Пользователь с таким логином отсутствует.", "Ошибка!", MessageBoxButton.OK);
                }
                else if (user.PASSWORD != password)
                    MessageBox.Show("Неверный пароль.", "Ошибка!", MessageBoxButton.OK);
                else
                {
                    try
                    {
                        MainFrame mainFrame = new MainFrame(user);
                        mainFrame.Show();
                        Window.GetWindow(this).Close();
                    }
                    catch
                    {
                        MessageBox.Show("Возникла ошибка. Пожалуйста, повторирте попытку поззже", "Ошибка!", MessageBoxButton.OK);
                    }
                }
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
