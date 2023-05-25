using System;
using System.IO;
using System.Windows;
using System.Xml;
using xelas_not_so_convenient_mysql_interface.Data;

namespace xelas_not_so_convenient_mysql_interface.Windows
{
    public partial class Settings : Window
    {
        private bool verbose_times;
        public Settings()
        {
            InitializeComponent();
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            JSONReadWrite json = new JSONReadWrite();
            xelas_not_so_convenient_mysql_interface.JSONClasses.Settings settings;

            settings = json.readSettingsJSON();
            if (settings.verbose_times)
            {
                checkbox_times_verbose.IsChecked = true;
            }
            else checkbox_times_verbose.IsChecked = false;
        }

        private void Window_closed(object sender, EventArgs e)
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string parsed_location = System.IO.Path.GetDirectoryName(location);

            JSONReadWrite json = new JSONReadWrite();
            xelas_not_so_convenient_mysql_interface.JSONClasses.Settings settings = new  xelas_not_so_convenient_mysql_interface.JSONClasses.Settings();

            if ((bool)checkbox_times_verbose.IsChecked)
            {
                settings.verbose_times = true;
            }
            else settings.verbose_times = false;

            json.writeSettingsJSON(settings);
        }
       
    }
}
