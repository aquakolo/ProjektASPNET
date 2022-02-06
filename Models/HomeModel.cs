using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class HomeModel
    {
        public List<ProductModel> Products { get; set; }
        public string Search { get; set; }
    }
}