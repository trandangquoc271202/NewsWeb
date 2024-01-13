using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsWeb.Models
{
    public class Model1 : DbContext
    {
        public Model1()
            : base("name=ProjectNet")
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<HistoryNews> HistoryNews { get; set; } 
    }
}