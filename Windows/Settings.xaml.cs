using System;
using System.IO;
using System.Windows;
using System.Xml;
using xelas_not_so_convenient_mysql_interface.Data;
using xelas_not_so_convenient_mysql_interface.Exc;

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
            ConfigRead read = new ConfigRead();

            read.readSettingsConf();

            if (read.VerboseTime)
            {
                checkbox_times_verbose.IsChecked = true;
            }
            else checkbox_times_verbose.IsChecked = false;
        }

        private void Window_closed(object sender, EventArgs e)
        {
            
            //TODO:ENABLE ON RELEASE
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string parsed_location = System.IO.Path.GetDirectoryName(location);


            XmlTextWriter xmlWriter = new XmlTextWriter(parsed_location + "\\Settings.xml", null);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            write("verbose_times",verbose_times.ToString(),xmlWriter);
            xmlWriter.Close();
            this.Hide();
        }

        private void write(string element, string value, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(element, "");
            xmlWriter.WriteString(value);
            xmlWriter.WriteEndElement();
        }

        public void write_defaults()
        {
            try
            {
                if (File.Exists("\\Settings.xml"))
                {
                    return;
                }

                string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string? parsed_location = Path.GetDirectoryName(location);
                if (parsed_location == null)
                {
                    throw new CodeFoxException();
                }
                XmlTextWriter xmlWriter = new XmlTextWriter(parsed_location + "\\Settings.xml", null);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                write("verbose_times", "True", xmlWriter);
                xmlWriter.Close();
            }
            catch (CodeFoxException e)
            {
                MessageBox.Show("Critical error!", "Critical error!", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            
        }

       
    }
}
