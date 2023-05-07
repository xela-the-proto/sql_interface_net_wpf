

using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class JSONReadWriteExperimental
    {

        private void writeConnectionJSON()
        {
            //TODO:rewrite this method
            Connection connection = new Connection("192.168.1.140", "employees", "root", "root");
            using (StreamWriter file = File.CreateText(".\\connection.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file,connection);
                MessageBox.Show("wrote to file");
            }
        }

        public Connection readConnectionJSON()
        {
            Connection? connection = new Connection();
            try
            {
                
                using (StreamReader file = File.OpenText(".\\connection.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    connection = (Connection)serializer.Deserialize(file, typeof(Connection));
                    if (connection == null)
                    {
                        throw new FileLoadException("The file is null!");
                    }
                }
            }
            catch (FileLoadException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return connection;
        }



    }
}
