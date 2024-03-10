using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioE.Models
{
    public class ItemForJsonCreatedModel
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string country_name { get; set; }
        public string http { get; set; } //acho que esse é o protocol que foi pedido
       
    }
}
