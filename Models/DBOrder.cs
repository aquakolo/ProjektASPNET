using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class DBOrder
    {
        [Key]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public DBUser User { get; set; }
        public string ProductsIDList { get; set; }
    }
}