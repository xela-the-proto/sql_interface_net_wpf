using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sql_interface_net_wpf.DB
{
    internal class DBUpload
    {
        private string conn_string;
        public DBUpload()
        {
            conn_string = string.Empty;
        }

        public DBUpload(string conn_string)
        {
            this.conn_string = conn_string;
        }

        public void Query(string command)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();
                MySqlCommand comm = new MySqlCommand();
                conn.ConnectionString = conn_string;
                conn.Open();
                comm.CommandText = command;
                comm.Connection = conn;
                comm.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
