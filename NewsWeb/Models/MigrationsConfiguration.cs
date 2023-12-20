using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace NewsWeb.Models
{
    internal sealed class MigrationsConfiguration : DbMigrationsConfiguration<Model1>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false; 
        }

        protected override void Seed(Model1 context)
        {
            
        }
    }
}