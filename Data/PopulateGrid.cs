using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;


namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class PopulateGrid
    {
        MySqlCommand comm = new MySqlCommand();
        Stopwatch stopwatch_query = new Stopwatch();
        Stopwatch stopwatch_population = new Stopwatch();
        public void initPopulator(MySqlCommand comm)
        {
            this.comm = comm;
        }
        
        public void populateGrid()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                window.query_data_grid.ItemsSource = null;
                window.query_data_grid.Items.Refresh();
                int rows_Affected = 0;
                try
                {
                    
                    MessageBox.Show("!!ATTENTION!!\n After closing this box the program might hang for a while depending on the query size\n Im experimenting in making it a thread", "warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    stopwatch_query.Start();
                    comm.ExecuteNonQuery();
                    stopwatch_query.Stop();
                    MySqlDataAdapter sda = new MySqlDataAdapter(comm);
                    DataTable dt = new DataTable("Query_result");
                    stopwatch_population.Start();
                    sda.Fill(dt);
                    window.query_data_grid.ItemsSource = dt.DefaultView;
                    stopwatch_population.Stop();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    TimeSpan query_elapsed = stopwatch_query.Elapsed;
                    TimeSpan population_elapsed = stopwatch_population.Elapsed;
                    if (comm.CommandText.Contains("SELECT"))
                    {
                        MessageBox.Show("Query done!\nTime to query " + query_elapsed +"\nTime to populate" + population_elapsed, "Query", MessageBoxButton.OK, MessageBoxImage.Question);
                        stopwatch_population.Reset();
                        stopwatch_query.Reset();
                    }
                    else
                    {
                        MessageBox.Show("Done! Lines affected " + rows_Affected + "", "Query", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                    
                }
            });

            
        }
    }
}
