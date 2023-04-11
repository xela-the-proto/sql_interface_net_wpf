using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Windows;

namespace xelas_not_so_convenient_mysql_interface.DB
{
    internal class ConnectionManager
    {
        MySqlConnection conn = new MySqlConnection();

        public void connect(string ip, string db_name, string user_id, string user_password)
        {
            try
            {
                conn.ConnectionString = "server=" + ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                conn.Open();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException e)
            {
                if (e.ToString().Contains("Cannot change the connection string on an open connection."))
                {
                    MessageBox.Show("cannot change connection whilst one is open!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void connect(string conn_string)
        {
            try
            {
                conn.ConnectionString = conn_string;   
                conn.Open();
                MainWindow mainWindow = new MainWindow();
                mainWindow.disable_buttons();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException e)
            {
                if (e.ToString().Contains("Cannot change the connection string on an open connection."))
                {
                    MessageBox.Show("cannot change connection whilst one is open!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void disconnect()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.enable_buttons();
        }

        public MySqlConnection getConnection()
        {
            return conn;
        }
    }
}
