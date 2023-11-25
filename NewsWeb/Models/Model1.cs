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

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.permission)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.address)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .Property(e => e.birthDay)
                .IsFixedLength();
        }
    }
}
