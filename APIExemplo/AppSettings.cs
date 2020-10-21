using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIExemplo
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Audencie { get; set; }
        public string Issuer { get; set; }
        public int Days { get; set; }
        public string Key { get; set; }

    }
}
