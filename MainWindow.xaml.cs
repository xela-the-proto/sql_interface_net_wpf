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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            connetionString = "Server=127.0.0.1,3306;Database=xela;User Id=xela;Password=xela;";

            DBUpload up = new DBUpload(connetionString);
            up.Query("CREATE TABLE IF NOT EXISTS Students (name TEXT(20), surname TEXT(20))");

        }
    }
}
