using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public bool Hide { get; set; }
    }
}