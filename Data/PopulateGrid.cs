using sql_interface_net_wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class PopulateGrid
    {
        public void populateColumns(DataTable schema)
        {
            MainWindow window = new MainWindow();

            foreach (DataRow row in schema.Rows)
            {
                /*
                DataGridTextColumn textColumn = new DataGridTextColumn();
                Binding b = new Binding(row.Field<string>("ColumnName"));
                textColumn.Binding = b;
                textColumn.Header = row.Field<string>("ColumnName");
                */
                window.query_data_grid.Columns.Add(new DataGridTextColumn() { Header = row.Field<string>("ColumnName") });
            }

        }
    }
}
