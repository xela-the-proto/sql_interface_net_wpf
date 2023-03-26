using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class PopulateGrid
    {
        DataTable schema = new DataTable();
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand comm = new MySqlCommand();
        public void initPopulator(DataTable schema, MySqlConnection conn, MySqlCommand comm)
        {
            this.schema = schema;
            this.conn = conn;
            this.comm = comm;
        }
        
        public async void populateGrid()
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            { 
                comm.ExecuteNonQuery();
                MySqlDataAdapter sda = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                window.query_data_grid.ItemsSource = dt.DefaultView;
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
