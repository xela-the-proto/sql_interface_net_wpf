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
        
        public void populateGrid()
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            int rows_Affected = 0;
            try
            {
                MessageBox.Show("!!ATTENTION!!\n After closing this box the program might hang for a while depending on the query size", "warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                rows_Affected = comm.ExecuteNonQuery();
                MySqlDataAdapter sda = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable("Query_result");
                sda.Fill(dt);
                window.query_data_grid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {

            } finally {
                MessageBox.Show("Done! Lines affected " + rows_Affected + "", "Query", MessageBoxButton.OK, MessageBoxImage.Question);
                    
             }
        }
    }
}
