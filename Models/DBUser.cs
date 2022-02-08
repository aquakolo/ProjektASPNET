using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class DBUser
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<DBCart> Carts { get; set; }
        public List<DBCart> Orders { get; set; }
    }
}