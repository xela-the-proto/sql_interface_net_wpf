﻿using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sql_interface_net_wpf.DB
{
    internal class DBUpload
    {
        private string conn_string;
        MySqlConnection conn = new MySqlConnection();
        
        public DBUpload()
        {
            conn_string = string.Empty;
        }

        public DBUpload(string conn_string)
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
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void connect(string ip,string db_name,string user_id,string user_password)
        {
            try
            {

                conn.ConnectionString = "server=" + ip + ";user id=" + user_id
                    + ";password=" + user_password + ";database=" + db_name + ";";
                conn.Open();

                MainWindow mainWindow = new MainWindow();
                mainWindow.txt_database_ip.IsEnabled = false;
                mainWindow.txt_database_name.IsEnabled = false;
                mainWindow.txt_database_user_id.IsEnabled = false;
                mainWindow.psw_user_database_password.IsEnabled = false;
                mainWindow.btn_connect.IsEnabled = false;
                mainWindow.btn_disconnect.IsEnabled = true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void disconnect()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.txt_database_ip.IsEnabled = true;
            mainWindow.txt_database_name.IsEnabled = true;
            mainWindow.txt_database_user_id.IsEnabled = true;
            mainWindow.psw_user_database_password.IsEnabled = true;
            mainWindow.btn_connect.IsEnabled = true;
            mainWindow.btn_disconnect.IsEnabled = false;
        }
    }
}
