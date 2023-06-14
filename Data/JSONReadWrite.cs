using Newtonsoft.Json;
using System.IO;
using System.Windows;
using xelas_not_so_convenient_mysql_interface.JSONClasses;
using xelas_not_so_convenient_mysql_interface.Windows;
using Settings = xelas_not_so_convenient_mysql_interface.JSONClasses.Settings;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class JSONReadWrite
    {
        public void writeConnectionJSON()
        {
            //TODO:rewrite this method
            string dir = ".\\Config\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            ConnectionWizard wizard  = new ConnectionWizard();
            wizard.ShowDialog();
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

        public Settings readSettingsJSON()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string parsed_location = System.IO.Path.GetDirectoryName(location);

            Settings? settings = new Settings();

            try
            {
                if (!File.Exists(".\\Config\\settings.json"))
                {
                    writeSettingsJSON(settings);
                }
                using (StreamReader file = File.OpenText(".\\Config\\settings.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    settings = (Settings)serializer.Deserialize(file, typeof(Settings));
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return settings;
        }

        public void writeSettingsJSON(Settings settings)
        {
            string dir = ".\\Config\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter file = File.CreateText(".\\Config\\settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, settings);
            }
        }
    }
}