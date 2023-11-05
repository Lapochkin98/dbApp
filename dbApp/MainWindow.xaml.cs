using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

namespace dbApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string connectionString = "Server=mysql-for-lapa-lapa-1.a.aivencloud.com;Port=20976;Database=ProductExhibition;User Id=avnadmin;Password=AVNS_pKJ67SyF9I3kScfiFh3;SslMode=Required;";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteQuery2(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT C.Name AS CompanyName, P.Name AS ProductName, COUNT(*) AS ExhibitionCount
FROM Companies C
JOIN ExhibitionOrganization EO ON C.CompanyID = EO.CompanyID
JOIN ExhibitionPlaces EP ON EO.PlaceID = EP.PlaceID
JOIN Products P ON P.ProductID = EO.ProductID
GROUP BY C.Name, P.Name;
";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(cmd.ExecuteReader());

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }

                statusBarLine.Text = "Успешно";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                statusBarLine.Text = "Произошла ошибка";
            }
        }

        private void ExecuteQuery1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT C.Name AS CompanyName, MAX(EP.Area) AS MaxArea
FROM Companies C
JOIN ExhibitionOrganization EO ON C.CompanyID = EO.CompanyID
JOIN ExhibitionPlaces EP ON EO.PlaceID = EP.PlaceID
GROUP BY C.Name";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(cmd.ExecuteReader());

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }

                statusBarLine.Text = "Успешно";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                statusBarLine.Text = "Произошла ошибка";
            }
        }

        private void ExecuteQuery3(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT P.Name AS ProductName, COUNT(*) AS ExhibitionCount, COUNT(DISTINCT C.CompanyID) AS CompanyCount
FROM Products P
JOIN ExhibitionOrganization EO ON P.ProductID = EO.ProductID
JOIN Companies C ON C.CompanyID = EO.CompanyID
GROUP BY P.Name;

";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(cmd.ExecuteReader());

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }

                statusBarLine.Text = "Успешно";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                statusBarLine.Text = "Произошла ошибка";
            }
        }

        private void ExecuteQuery4(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT YEAR(EO.StartDate) AS ExhibitionYear, COUNT(*) AS ExhibitionCount, COUNT(P.ProductID) AS TotalProducts
FROM ExhibitionOrganization EO
JOIN Products P ON P.ProductID = EO.ProductID
GROUP BY ExhibitionYear;
";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(cmd.ExecuteReader());

                    dataGrid.ItemsSource = dataTable.DefaultView;
                }

                statusBarLine.Text = "Успешно";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                statusBarLine.Text = "Произошла ошибка";
            }
        }
    }
}
