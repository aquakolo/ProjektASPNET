using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class OrdersModel
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
    }
}