using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioE.Models
{
    public class FilePathReadItemModel
    {
        public string host { get; set; }
        public string ip { get; set; }
        public string port { get; set; }
        public string lastseen { get; set; }
        public string delay { get; set; }
        public string cid { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string city { get; set; }
        public string checks_up { get; set; }
        public string checks_down { get; set; }
        public string anon { get; set; }
        public string http { get; set; }
        public string ssl { get; set; }
        public string socks4 { get; set; }
        public string socks5 { get; set; }
       
    }
}
