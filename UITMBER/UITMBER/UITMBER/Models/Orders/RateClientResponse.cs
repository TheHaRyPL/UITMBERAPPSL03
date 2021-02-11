using System;
using System.Collections.Generic;
using System.Text;

namespace UITMBER.Models.Orders
{
    public class RateClientResponse
    {
        public long? IdDriver { get; set; }
        public bool Success { get; set; }
        public double? Rate { get; set; }
        public string Info { get; set; }
        public DateTime? Date { get; set; }
        public string Error { get; set; }
    }
}

