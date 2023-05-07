using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xelas_not_so_convenient_mysql_interface.Data
{
    internal class Connection
    {
        /*
         *private string db_ip;
          private string db_name;
          private string user_id;
          private string user_password;
          private string dns_text = "";
         *
        */
        public Connection(string db_ip, string db_name, string user_id, string user_password)
        {
            this.db_ip = db_ip;
            this.db_name = db_name;
            this.user_id = user_id;
            this.user_password = user_password;
        }

        public Connection()
        {
            db_ip = "";
            db_name = "";
            user_id = "";
            user_password = "";
        }

        public string db_ip { get; set; }
        public string db_name { get; set;}
        public string user_id { get; set; }
        public string user_password { get; set; }
    }
}
