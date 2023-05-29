using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using xelas_not_so_convenient_mysql_interface.JSONClasses;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class PopulateGrid
    {
        private MySqlCommand comm = new MySqlCommand();
        private xelas_not_so_convenient_mysql_interface.JSONClasses.Settings settings = new Settings();
        private Stopwatch stopwatch_query = new Stopwatch();
        private Stopwatch stopwatch_population = new Stopwatch();
        private JSONReadWrite json = new JSONReadWrite();

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

                settings = json.readSettingsJSON();
                int rows_Affected = 0;
                try
                {
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
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
                TimeSpan query_elapsed = stopwatch_query.Elapsed;
                TimeSpan population_elapsed = stopwatch_population.Elapsed;
                if (comm.CommandText.Contains("SELECT"))
                {
                    /*
                    MessageBox.Show("Query done!\nTime to query " + query_elapse    +"\nTimetopopulate"+population_elapsed,"Query",MessageBoxButton.OK, MessageBoxImage.Question);
                    */
                    stopwatch_population.Reset();
                    stopwatch_query.Reset();

                    if (settings.verbose_times)
                    {
                        window.status_s_query.Text = "Last population time =" + query_elapsed.TotalSeconds + "s";
                        window.status_s_popul.Text = "Last query time =" + population_elapsed.TotalSeconds + "s";
                    }
                    else if (!settings.verbose_times)
                    {
                        window.status_s_query.Text = "Last population time =" + query_elapsed.Seconds + "s";
                        window.status_s_popul.Text = "Last query time =" + population_elapsed.Seconds + "s";
                    }
                }
                else
                {
                    MessageBox.Show("Done! Lines affected " + rows_Affected + "","Query",MessageBoxButton.OK,MessageBoxImage.Question);
                }
                
            });
        }
    }
}