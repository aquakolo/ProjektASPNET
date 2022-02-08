using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class DBCart
    {
        [Key]
        public int CartID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public DBUser User { get; set; }
        public int ProductID{ get; set; }
        [ForeignKey("ProductID")]
        public DBProduct Product { get; set; }
    }
}