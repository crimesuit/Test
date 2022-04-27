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
using MaterialDesignThemes.Wpf;
using System.Data.SqlClient;
using System.Data;


namespace Test
{
    /// <summary>
    /// Логика взаимодействия для Panel.xaml
    /// </summary>
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Panel : Window
    {
        DataBase dataBase = new DataBase();

        int selectedRow;

        public Panel()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=Roman;Initial Catalog=Base;Integrated Security=True");
        public void LoadGrid()
        {
            SqlCommand command = new SqlCommand("select * from base_db", sqlConnection);
            DataTable dt = new DataTable();
            sqlConnection.Open();
            SqlDataReader sdr = command.ExecuteReader();
            dt.Load(sdr);
            sqlConnection.Close();
            datagrid.ItemsSource = dt.DefaultView;

        }

        public void createColumm()
        {

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
        public void clearData()
        {
            txtType_of.Clear();
            txtPostavka.Clear();
            txtPrice.Clear();
            txtCount.Clear();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ReadSingleRows(DataGrid dwg, IDataRecord record)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Запись успешно добавлена в БД");
        }
        public bool IsValid()
        {
            if (txtType_of.Text == string.Empty)
            {
                MessageBox.Show("Тип занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPostavka.Text == string.Empty)
            {
                MessageBox.Show("Тип поставки занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPrice.Text == string.Empty)
            {
                MessageBox.Show("Цена занята", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCount.Text == string.Empty)
            {
                MessageBox.Show("Допустимое количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    SqlCommand command = new SqlCommand("INSERT INTO base_db VALUES (@type_of, @count_of, @postavka, @price)", sqlConnection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@type_of", txtType_of.Text);
                    command.Parameters.AddWithValue("@count_of", txtCount.Text);
                    command.Parameters.AddWithValue("@postavka", txtPostavka.Text);
                    command.Parameters.AddWithValue("@price", txtPrice.Text);
                    

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                    LoadGrid();
                    MessageBox.Show("Успешно добавлено", "Сохранено", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("delete from base_db where ID = "+textBox_search.Text+" ", sqlConnection);
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись удалена", "Удалено",  MessageBoxButton.OK, MessageBoxImage.Information);
                sqlConnection.Close();
                clearData();
                LoadGrid();
                sqlConnection.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Не удалено" +ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
