using sql_interface_net_wpf.DB;
using System;
using System.Collections.Generic;
using System.Windows;


namespace sql_interface_net_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        QueryDb db = new QueryDb();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            db.connect(txt_database_ip.Text, txt_database_name.Text, txt_database_user_id.Text, psw_user_database_password.Password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.Query(txt_sql_command.Text);
        }

        public void disable_buttons() {
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            db.connect();
        }

        private void btn_disconnect_Click(object sender, RoutedEventArgs e)
        {
            enable_buttons();
            db.disconnect();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
