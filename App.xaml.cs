using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using xelas_not_so_convenient_mysql_interface.Windows;

namespace sql_interface_net_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            Settings settings = new Settings();
            settings.write_defaults();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
