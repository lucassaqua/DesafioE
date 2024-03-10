using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioE.Domain
{
    public class Proxys
    {
        public int Id { get; set; }
        public DateTime StartTimeExecution { get; set; }
        public DateTime ExecutionEndTime { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfLines { get; set; }
        public string ListToSave { get; set; }
    }
}
