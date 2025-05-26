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
using MySql.Data.MySqlClient;

namespace filmek_doga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost ; database = kovacsk1; uid = root; password =''; charset= utf8;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            var reader = new MySqlCommand($"SELECT * FROM filmek", kapcs).ExecuteReader();
            while (reader.Read())
            {
                
                lbAdatok.Items.Add(reader["filmazon"] + ";" + reader["cim"] + ";" + reader["ev"] + ";" + reader["szines"] + ";" + reader["mufaj"] + ";" + reader["hossz"]);
            }
            reader.Close();
            kapcs.Close();
        }

        private void lbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                var sor = ((string)lbAdatok.SelectedItem).Split(';');
                lbFilmAzon.Content = sor[0];
                tb1.Text = sor[1];
                tb2.Text = sor[2];
                tb3.Text = sor[3];
                tb4.Text = sor[4];
                tb5.Text = sor[5];
            }
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var modositTomb = new string[1];
            modositTomb[0] = lbAdatok.Items[0].ToString();

            kapcs.Open();
            var sql = $"UPDATE filmek SET cim = '{tb1.Text}',ev = '{tb2.Text}',szines = '{tb3.Text}',mufaj = '{tb4.Text}', hossz = {tb5.Text}  WHERE filmazon = {lbFilmAzon.Content}";
            new MySqlCommand(sql, kapcs).ExecuteNonQuery();
            MessageBox.Show("Sikeres módosítás!");
            lbAdatok.Items.Clear();
            var reader = new MySqlCommand($"SELECT * FROM filmek", kapcs).ExecuteReader();
            while (reader.Read())
            {
               lbAdatok.Items.Add(reader["filmazon"] + ";" + reader["cim"] + ";" + reader["ev"] + ";" + reader["szines"] + ";" + reader["mufaj"] + ";" + reader["hossz"]);
            }
            reader.Close();
            kapcs.Close();
        }
    }
}
