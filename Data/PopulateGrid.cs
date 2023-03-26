using MySqlConnector;
using sql_interface_net_wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class PopulateGrid
    {
        DataTable schema = new DataTable();
        MySqlConnection conn = new MySqlConnection();
        MySqlCommand comm = new MySqlCommand();
        public void initPopulator(DataTable schema, MySqlConnection conn, MySqlCommand comm)
        {
            this.schema = schema;
            this.conn = conn;
            this.comm = comm;
        }
        //Fix
        public void populateColumns()
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            try
            { 
                comm.ExecuteNonQuery();
                MySqlDataAdapter sda = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                window.query_data_grid.ItemsSource = dt.DefaultView;

            }
            catch (Exception ex)
            {

            }

            /*
            
            var reader = comm.ExecuteReader();

            schema = reader.GetSchemaTable();

            for (int i = 0; i < schema.Columns.Count; i++)    
            {
                window.SetColumn(schema.Columns[i].ColumnName.ToString());
                MessageBox.Show(schema.Columns[i].ColumnName.ToString());
            }
            reader.Close();
            */
        }

        public void populateRows()
        {
            string total_query = "";
            var reader = comm.ExecuteReader();

            while (reader.Read())
            {
                string values;
                string?[] values_array = new string[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values_array[i] = reader.GetValue(i).ToString();
                }

                values = string.Join("          ", values_array);

                total_query = total_query + values;
            }
            reader.Close();
        }
    }
}
