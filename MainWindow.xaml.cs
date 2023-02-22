using sql_interface_net_wpf.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sql_interface_net_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QueryDb up = new QueryDb();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            up.connect(txt_database_ip.Text, txt_database_name.Text, txt_database_user_id.Text, psw_user_database_password.Password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            up.Query(txt_sql_command.Text);
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
    }
}
