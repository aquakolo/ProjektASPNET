﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public int User { get; set; }
        public List<ProductModel> Products { get; set; }
        public int TotalPrice { get; set; }
    }
}