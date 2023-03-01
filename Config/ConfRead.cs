using Microsoft.Win32;
using System;
using System.Windows;
using System.Xml.Linq;

namespace sql_interface_net_wpf.Config
{
    internal class ConfRead
    {
        private string db_ip, db_name, user_id, user_password;
        private bool debug_msgbox = false;
        private string debug_msgbox_text;

        public string getIp()
        {
            return db_ip;
        }

        public string getName()
        {
            return db_name;
        }

        public string getId()
        {
            return user_id;
        }

        public string getPass()
        {
            return user_password;
        }


        public void readConnConfig()
        {
            OpenFileDialog file_open = new OpenFileDialog();
            file_open.Filter = "xelafml (*.xelafml)| *.xelafml";
            file_open.ShowDialog();
            XDocument reader = XDocument.Load(file_open.FileName);
            foreach (var ip in reader.Descendants("dbip"))
            {
               db_ip = (string) ip.Attribute("ip");
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
                if (debug_msgbox_text.ToLower() == "true") {
                    debug_msgbox = true;
                }
            }

            if (debug_msgbox)
            {
                MessageBox.Show(db_ip + " " + db_name + " " + user_id + " " + user_password, "debug");
            }
            
            
        }


       
    }
}
