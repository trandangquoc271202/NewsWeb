using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NewsWeb.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.permission)
                .IsFixedLength();
        }
    }
}
