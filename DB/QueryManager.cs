using Microsoft.VisualBasic;
using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml.Linq;
using xelas_not_so_convenient_mysql_interface.Data;
using xelas_not_so_convenient_mysql_interface.DB;

namespace sql_interface_net_wpf.DB
{
    internal class QueryManager
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand comm = new MySqlCommand();
        DataTable schema = new DataTable();
        PopulateGrid populate = new PopulateGrid();
        public QueryManager()
        {

        }


        public void Query(string command, ConnectionManager conn_manager)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            {
                comm.CommandText = command;
                comm.Connection = conn_manager.getConnection();
                //TODO:more support to other commands
                int totalCount = comm.Parameters.Count;
                string total_query = "";
                if (comm.CommandText.Contains("SELECT"))
                {
                    window.query_progress.IsEnabled  =true;
                    populate.initPopulator(schema, conn_manager.getConnection(), comm);
                    new Thread(new ThreadStart(populate.populateGrid)).Start();

                }
                else
                {
                    int rows = comm.ExecuteNonQuery();
                    MessageBox.Show("Done! " + rows + " rows have been affected!", "Query", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch(InvalidOperationException e)
            {
                if (e.Message.Contains("CommandText must be specified"))
                {
                    MessageBox.Show("Missing mysql comamnd!","Query",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private async Task QueryParse(MySqlCommand command)
        {
            int totalCount = command.Parameters.Count;
            string total_query = "";
            if (command.CommandText.Contains("SELECT"))
            {
                using var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    string values;
                    string?[] values_array = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        values_array[i] = reader.GetValue(i).ToString();
                    }

                    values = string.Join("          ", values_array);


                    total_query = total_query + values;
                }
                //TODO: for big queries messageboxes are shit
                 MessageBox.Show("Query results\n" + total_query, "Query", MessageBoxButton.OK, MessageBoxImage.Information);
                if (!reader.HasRows)
                {
                    MessageBox.Show("No rows found that matched the query", "Query", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                int rows = comm.ExecuteNonQuery();
                MessageBox.Show("Done! " + rows + " rows have been affected!", "Query", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void SaveTable(string command,string collection)
        {
            try
            {
                DataTable table = conn.GetSchema();
                //TODO: save tables to disk
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
