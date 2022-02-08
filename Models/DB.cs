using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjektASPNET.Models
{
    public class DB : DbContext
    {
        public DbSet<DBUser> Users { get; set; }
        public DbSet<DBProduct> Products { get; set; }
        public DbSet<DBCart> Carts { get; set; }
        public DbSet<DBOrder> Orders { get; set; }
    }
}