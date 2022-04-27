using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using MaterialDesignThemes.Wpf;
using Test;
using System.Data.SqlClient;
using System.Data;

namespace Test
{
    public partial class MainWindow : Window
    {
        DataBase dataBase = new DataBase();
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {     
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }

            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);

        }
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void LoginR(object sender, RoutedEventArgs e)
        {
            var login = Username.Text.Trim();
            var pass1 = Password.Password.Trim();
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user='{login}' and password_user='{pass1}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);


            if (login.Length < 5) // Длинна
            {
                Username.ToolTip = "Попробуйте другой логин";
                Username.Background = Brushes.Red;
            }
            else if (pass1.Length < 5)
            {
                Password.ToolTip = "Слишком короткий пароль!";
                Password.Background = Brushes.Red;
            }
            else
            {
                Username.ToolTip = "";
                Username.Background = Brushes.Transparent;

                Password.ToolTip = "";
                Password.Background = Brushes.Transparent;


            }
            if (table.Rows.Count == 1)
            {

                Panel panel = new Panel();
                panel.Show();
                Hide();
            }
            else
                MessageBox.Show("Аккаунта не существует", "Попробуйте другой аккаунт", MessageBoxButton.OK);

        }
        private void txtPassword_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {

        }
        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            Hide();
        }
    }
}
