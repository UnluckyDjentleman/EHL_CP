using System;
using System.Collections.Generic;
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
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.LanguageChanged += languageChanged;
            CultureInfo currLang = App.Language;
        }
        private void languageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;
        }

        private void blr_lang_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo lang = new CultureInfo("be-BY");
            App.Language = lang;
        }

        private void rus_lang_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo lang = new CultureInfo("ru-RU");
            App.Language = lang;
        }

        private void uk_lang_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo lang = new CultureInfo("en-US");
            App.Language = lang;
        }
    }
}
