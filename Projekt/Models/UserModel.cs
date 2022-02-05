using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektASPNET.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}