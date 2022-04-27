using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using Test;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataBase dataBase = new DataBase();
        public Window1()
        {
            InitializeComponent();
         
        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void PasswordBox_Password2(object sender, RoutedEventArgs e)
        {

        }

        private void doLogin(object sender, RoutedEventArgs e)
        {

        }
        private void txtPassword(object sender, RoutedEventArgs e)
        {

        }
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
        private void Password1(object sender, RoutedEventArgs e)
        {

        }
        private void Password2(object sender, RoutedEventArgs e)
        {

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
        private void txtPassword_ContextMenuClosing(object sender, RoutedEventArgs e)
        {

        }
        private void txtUsername_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {

            var login = Username.Text.Trim();
            var password1 = pass1.Password.Trim();
            var password2 = pass2.Password.Trim();

            string querystring = $"insert into register(login_user, password_user, password2_user) values('{login}', '{password1}', '{password2}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());
            if (checkuser())

            {
                return;
            }
            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешно создан аккаунт");
            }
            else
            {
                MessageBox.Show("Аккаунт не зарегестрирован");
            }

            dataBase.closeConnection();

            if (login.Length < 6)
            {
                Username.ToolTip = "Слишком маленький логин";
                Username.Background = Brushes.Red;
            }

            if (password1.Length < 6 )
            {
                pass1.ToolTip = "Попробуйте другой пароль (Попробуйте использовать знаки !, .)";
                pass1.Background = Brushes.Red;
            }

            else if (password2.Length < 6)
            {
                pass2.ToolTip = "Попробуйте другой пароль (Попробуйте использовать обязательные знаки !, . )";
                pass2.Background = Brushes.Red;
            }

            else if(password1 != password2)
            {
                pass2.ToolTip = "Пароли не совпадают";
                pass2.Background = Brushes.Red;
            }
            else
            {
                Username.ToolTip = "";
                Username.Background = Brushes.Transparent;

                pass1.ToolTip = "";
                pass1.Background = Brushes.Transparent;

                pass2.ToolTip = "";
                pass2.Background = Brushes.Transparent;

            }
        }

        private Boolean checkuser()
        {
            var loginUser = Username.Text.Trim();
            var password1User = pass1.Password.Trim();
            var password2User = pass2.Password.Trim();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user, password2_user from register where login_user ='{loginUser}' and password_user = '{password1User}' and password2_user ='{password2User}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else 
            {
                return false;
            }
        }
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            Hide();
        }

    }
}
