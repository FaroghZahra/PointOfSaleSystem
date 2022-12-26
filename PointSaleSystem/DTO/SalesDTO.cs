using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SalesDTO
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string status { get; set; }
    }
}
