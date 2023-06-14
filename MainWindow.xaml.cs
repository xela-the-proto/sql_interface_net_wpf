using sql_interface_net_wpf.DB;
using System;
using System.IO;
using System.Windows;
using xelas_not_so_convenient_mysql_interface.Data;
using xelas_not_so_convenient_mysql_interface.DB;
using xelas_not_so_convenient_mysql_interface.JSONClasses;

namespace sql_interface_net_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        private QueryManager queryDb = new QueryManager();
        private JSONReadWrite jsonReadWrite = new JSONReadWrite();

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(".\\Config\\connection.json"))
            {
                jsonReadWrite.writeConnectionJSON();
            }
        }

        private void Json_connect(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            Connection connection = new Connection();
            connection = jsonReadWrite.readConnectionJSON();
            connectionManager.connect(connection.getConnString());
        }

        private void Manual_connect(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            connectionManager.connect(txt_database_ip.Text, txt_database_name.Text, txt_database_user_id.Text, psw_user_database_password.Password);
        }

        private void Query(object sender, RoutedEventArgs e)
        {
            queryDb.Query(txt_sql_command.Text, connectionManager);
        }

        public void disable_buttons()
        {
            txt_database_ip.IsEnabled = false;
            txt_database_name.IsEnabled = false;
            txt_database_user_id.IsEnabled = false;
            psw_user_database_password.IsEnabled = false;
            btn_connect.IsEnabled = false;
            btn_disconnect.IsEnabled = true;
        }

        public void enable_buttons()
        {
            txt_database_ip.IsEnabled = true;
            txt_database_name.IsEnabled = true;
            txt_database_user_id.IsEnabled = true;
            psw_user_database_password.IsEnabled = true;
            btn_connect.IsEnabled = true;
            btn_disconnect.IsEnabled = false;
        }

        private void btn_disconnect_Click(object sender, RoutedEventArgs e)
        {
            enable_buttons();
            connectionManager.disconnect();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_clear_datagrid_Click(object sender, RoutedEventArgs e)
        {
            query_data_grid.ItemsSource = null;
            query_data_grid.Items.Refresh();
        }

        private void open_settings_Click_1(object sender, RoutedEventArgs e)
        {
            xelas_not_so_convenient_mysql_interface.Windows.Settings settings = new xelas_not_so_convenient_mysql_interface.Windows.Settings();

            settings.Show();
        }
    }
}