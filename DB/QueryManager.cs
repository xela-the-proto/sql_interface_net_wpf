using Microsoft.VisualBasic;
using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Data;
using System.Linq;
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
        private string db_ip;
        private string db_name;
        private string user_id;
        private string user_password;
        private string dns_text = "";
        private bool debug_msgbox;
        private bool dns;
        private string debug_msgbox_text;
        private string conn_string;
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand comm = new MySqlCommand();
        DataTable schema = new DataTable();
        PopulateGrid populate = new PopulateGrid();
        public QueryManager()
        {
            conn_string = string.Empty;
        }

        public QueryManager(string conn_string)
        {
            this.conn_string = conn_string;
        }

        public void Query(string command, ConnectionManager conn_manager)
        {
            MainWindow window = new MainWindow();
            int j = 0;
            try
            {
                conn = conn_manager.getConnection();
                comm.CommandText = command;
                comm.Connection = conn_manager.getConnection();
                //TODO:perfect query messages
                int totalCount = comm.Parameters.Count;
                string total_query = "";
                if (comm.CommandText.Contains("SELECT"))
                {
                    using var reader = comm.ExecuteReader();
                    schema = reader.GetSchemaTable();
                    populate.populateColumns(schema);
                    foreach (DataRow row in schema.Rows)
                    {
                        /*
                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        Binding b = new Binding(row.Field<string>("ColumnName"));
                        textColumn.Binding = b;
                        textColumn.Header = row.Field<string>("ColumnName");
                        */
                        window.query_data_grid.Columns.Add(new DataGridTextColumn() { Binding = new Binding("ColumnName={"+ j +"}"), Header = row.Field<string>("ColumnName") });
                    }
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
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
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

        //TODO: handle a loading page
        /*
        private void processing_window_open()
        {
            loading_widnow.Show();
        }

        private void processing_window_close()
        {
            loading_widnow.Hide();
        }
        */
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
