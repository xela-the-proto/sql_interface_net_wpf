using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xelas_not_so_convenient_mysql_interface.JSONClasses
{
    internal class Settings
    {
        public bool verbose_times { get; set; }

        public Settings(bool verbose_times) { 
            this.verbose_times = verbose_times;
        }

        public Settings()
        {
            verbose_times = false;
        }
    }
}
