using Microsoft.Win32;
using System;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class ConfigRead
    {
        private string db_ip;
        private string db_name;
        private string user_id;
        private string user_password;
        private string dns_text = "";
        private bool debug_msgbox;
        private bool dns;
        private string debug_msgbox_text;
        private string conn_string;

        public bool verbose_time;



        public void readConnConfig()
        {
            try
            {
                OpenFileDialog file_open = new OpenFileDialog();
                file_open.Filter = "xlafml (*.xlafml)| *.xlafml";
                file_open.ShowDialog();
                XDocument reader = XDocument.Load(file_open.FileName);
                foreach (var ip in reader.Descendants("dbip"))
                {
                    db_ip = (string)ip.Attribute("ip");
                }
                foreach (var id in reader.Descendants("userid"))
                {
                    user_id = (string)id.Attribute("id");
                }
                foreach (var password in reader.Descendants("userpassword"))
                {
                    user_password = (string)password.Attribute("password");
                }
                foreach (var name in reader.Descendants("dbname"))
                {
                    db_name = (string)name.Attribute("name");
                }
                foreach (var debug_box in reader.Descendants("debugtxtbox"))
                {
                    debug_msgbox_text = (string)debug_box.Attribute("debug");
                    if (debug_msgbox_text.ToLower() == "true")
                    {
                        debug_msgbox = true;
                    }
                    else dns = false;
                }
                foreach (var dns in reader.Descendants("dns"))
                {
                    if (dns_text.ToLower() == "true")
                    {
                        this.dns = true;
                    }
                    else this.dns = false;
                }
                if (debug_msgbox)
                {
                    MessageBox.Show(db_ip + " " + db_name + " " + user_id + " " + user_password, "debug");
                }
            }
            catch (ArgumentException e)
            {
                if (e.ToString().Contains("The string was not recognized as a valid Uri"))
                {
                    MessageBox.Show("no file was selected!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
        }

        public string getConnString()
        {
            return "server=" + db_ip + ";user id=" + user_id + ";password=" + user_password + ";database=" + db_name + ";";
        }

        public void readSettingsConf()
        {
            //TODO:ENABLE ON RELEASE
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string parsed_location = System.IO.Path.GetDirectoryName(location);

            XmlReader reader = new XmlTextReader(parsed_location + "\\Settings.xml");

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    //return only when you have START tag  
                    switch (reader.Name)
                    {
                        case "verbose_times":
                            if (reader.ReadElementContentAsString() == "True")
                            {
                                verbose_time = true;
                            }
                            else verbose_time = false;
                            break;
                    }
                }
            }

            reader.Close();
        }


        public bool VerboseTime
        {
            get => verbose_time;
            set => verbose_time = value;
        }
    }
}
