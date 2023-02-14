using MySql.Data.MySqlClient;
using Mysqlx;
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
        MySqlConnection conn = new MySqlConnection();
        
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
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = command;
                comm.Connection = conn;
                comm.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void connect(string ip,string db_name,string user_id,string user_password)
        {
            try
            {

                conn.ConnectionString = "server=" + ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                conn.Open();

                MainWindow mainWindow = new MainWindow();
                mainWindow.disable_buttons();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void disconnect()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.enable_buttons();
        }
    }
}
