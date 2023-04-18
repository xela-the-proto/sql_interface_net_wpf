using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace xelas_not_so_convenient_mysql_interface.Windows
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private bool verbose_times;
        public Settings()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (checkbox_times_verbose.IsChecked == true)
            {
                verbose_times = true;
            }
            else
            {
                verbose_times = false;
            }
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
    }
}
