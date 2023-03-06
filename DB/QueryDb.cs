using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Mysqlx;
using sql_interface_net_wpf.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace sql_interface_net_wpf.DB
{
    internal class QueryDb
    {
        private string db_ip;
        private string db_name;
        private string user_id;
        private string user_password;
        private bool debug_msgbox; 
        private string debug_msgbox_text;
        private string conn_string;
        MySqlConnection conn = new MySqlConnection();
        ConfRead config = new ConfRead();
        
        public QueryDb()
        {
            conn_string = string.Empty;
        }

        public QueryDb(string conn_string)
        {
            this.conn_string = conn_string;
        }

        public void Query(string command)
        {
            try
            {
                MySqlCommand comm = new MySqlCommand();
                comm.CommandText = command;
                comm.Connection = conn;
                comm.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void SaveTable(string command,string collection)
        {
            try
            {
                DataTable table = conn.GetSchema();
                //TODO: save tables to disk
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void connect(string ip,string db_name,string user_id,string user_password)
        {
            try
            {
                readConnConfig();
                conn.ConnectionString = "server=" + ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                conn.Open();
                
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void connect()
        {
            try
            {
                readConnConfig();

                conn.ConnectionString = "server=" + db_ip +  ";uid=" + user_id
                    + ";pwd=" + user_password + ";database=" + db_name + ";";
                MessageBox.Show("server=" + db_ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" +db_name + ";","Debug");
                //TODO: fix 'Object cannot be cast from DBNull to other types.' when connecting to server
                conn.Open();

                MainWindow mainWindow = new MainWindow();
                mainWindow.disable_buttons();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void disconnect()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.enable_buttons();
        }

        public void readConnConfig()
        {
            OpenFileDialog file_open = new OpenFileDialog();
            file_open.Filter = "xelafml (*.xelafml)| *.xelafml";
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
            }
            if (debug_msgbox)
            {
                MessageBox.Show(db_ip + " " + db_name + " " + user_id + " " + user_password, "debug");
            }
        }
    }
}
