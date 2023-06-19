using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Linq;
using System.Windows;

namespace xelas_not_so_convenient_mysql_interface.DB
{
    internal class ConnectionManager
    {
        private MySqlConnection conn = new MySqlConnection();

        public void connect(string ip, string db_name, string user_id, string user_password)
        {
            MainWindow? window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            {
                if (window == null)
                {
                    throw new ArgumentNullException();
                }
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
                    MessageBox.Show("cannot change connection whilst one is open!", "Error!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("Critical error! Program has to shut down!", "Critical Error!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            window.status_text_connection.Text = "Connection status: Open";
        }

        public void connect(string conn_string)
        {
            MainWindow? window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            {
                if (window == null)
                {
                    throw new ArgumentNullException();
                }
                conn.ConnectionString = conn_string;
                conn.Open();
                window.disable_buttons();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException e)
            {
                if (e.ToString().Contains("Cannot change the connection string on an open connection."))
                {
                    MessageBox.Show("cannot change connection whilst one is open!", "Error!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            window.status_text_connection.Text = "Connection status: Open";
        }

        public void disconnect()
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            conn.Close();
            window.enable_buttons();
            window.status_text_connection.Text = "Connection status: Closed";
        }

        public MySqlConnection getConnection()
        {
            return conn;
        }
    }
}