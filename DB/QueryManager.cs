using MySqlConnector;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using xelas_not_so_convenient_mysql_interface.Data;
using xelas_not_so_convenient_mysql_interface.DB;

namespace sql_interface_net_wpf.DB
{
    internal class QueryManager
    {
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand comm = new MySqlCommand();
        DataTable schema = new DataTable();
        PopulateGrid populate = new PopulateGrid();
        public QueryManager()
        {

        }


        public void Query(string command, ConnectionManager conn_manager)
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            {
                comm.CommandText = command;
                comm.Connection = conn_manager.getConnection();
                //TODO:more support to other commands
                int totalCount = comm.Parameters.Count;
                if (comm.CommandText.Contains("SELECT"))
                {
                    populate.initPopulator(comm);
                    //might remove multithreadign all together
                    Thread grid_thread = new Thread(new ThreadStart(populate.populateGrid));
                    grid_thread.SetApartmentState(ApartmentState.MTA);
                    grid_thread.Start();
                }
                else
                {
                    int rows = comm.ExecuteNonQuery();
                    MessageBox.Show("Done! " + rows + " rows have been affected!", "Query", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch(InvalidOperationException e)
            {
                if (e.Message.Contains("CommandText must be specified"))
                {
                    MessageBox.Show("Missing mysql command!","Query",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        //this method is pretty stupid for large queries but idk might move it somewhere
        //on second thought it makes 0 sense but meh why not
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

    }
}
