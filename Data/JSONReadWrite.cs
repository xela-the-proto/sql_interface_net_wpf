using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class JSONReadWriteExperimental
    {

        public void writeConnectionJSON()
        {
            //TODO:rewrite this method
            string dir = ".\\Config\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            Connection connection = new Connection("192.168.1.140", "employees", "root", "root");
            using (StreamWriter file = File.CreateText(".\\Config\\connection.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file,connection);
                MessageBox.Show("wrote to file");
            }
        }

        public Connection readConnectionJSON()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string parsed_location = System.IO.Path.GetDirectoryName(location);

            Connection? connection = new Connection();
            try
            {
                using (StreamReader file = File.OpenText(".\\Config\\connection.json"))
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
