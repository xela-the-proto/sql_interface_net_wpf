using Microsoft.VisualBasic;
using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace sql_interface_net_wpf.DB
{
    internal class QueryDb
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

        public QueryDb()
        {
            conn_string = string.Empty;
        }

        public QueryDb(string conn_string)
        {
            this.conn_string = conn_string;
        }

        public async void QueryParse(string command)
        {
            try
            {
                
                comm.CommandText = command;
                comm.Connection = conn;
                //TODO:pefect query messages
                if (command.Contains("SELECT"))
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

                        values = string.Join("      ", values_array);

                        MessageBox.Show("Query results\n" + values, "Query", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No rows found that matched the query", "Query warning", MessageBoxButton.OK, MessageBoxImage.Hand);
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

        public void connect(string ip,string db_name,string user_id,string user_password)
        {
            try
            {
                conn.ConnectionString = "server=" + ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                conn.Open();
                
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void connect()
        {
            try
            {

                readConnConfig();

                if (dns)
                {
                    conn.ConnectionString = "server=" + db_ip + ";dns-srv=true; uid=" + user_id
                    + ";pwd=" + user_password + ";database=" + db_name + ";";
                }
                else if (!dns)
                {
                    conn.ConnectionString = "server=" + db_ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                }
                conn.Open();

                MainWindow mainWindow = new MainWindow();
                mainWindow.disable_buttons();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void disconnect()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.enable_buttons();
        }

        public void readConnConfig()
        {
            OpenFileDialog file_open = new OpenFileDialog();
            file_open.Filter = "xelafml (*.xelafml)| *.xelafml";
            file_open.ShowDialog();
            XDocument reader = XDocument.Load(file_open.FileName);
            foreach (var ip in reader.Descendants("dbip"))
            {
                db_ip = (string)ip.Attribute("ip");
            }
            foreach (var id in reader.Descendants("userid"))
            {
                user_id = (string)id.Attribute("id");
            }
            foreach (var password in reader.Descendants("userpassword"))
            {
                user_password = (string)password.Attribute("password");
            }
            foreach (var name in reader.Descendants("dbname"))
            {
                db_name = (string)name.Attribute("name");
            }
            foreach (var debug_box in reader.Descendants("debugtxtbox"))
            {
                debug_msgbox_text = (string)debug_box.Attribute("debug");
                if (debug_msgbox_text.ToLower() == "true")
                {
                    debug_msgbox = true;
                }
                else dns = false;
            }
            foreach(var dns in reader.Descendants("dns"))
            {
                if (dns_text.ToLower() == "true")
                {
                    this.dns = true;
                }
                else this.dns = false;
            }
            if (debug_msgbox)
            {
                MessageBox.Show(db_ip + " " + db_name + " " + user_id + " " + user_password, "debug");
            }
        }
    }
}
