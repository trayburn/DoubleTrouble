using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoubleTrouble
{
    public class DbContext6 : System.Data.Entity.DbContext
    {
        public DbContext6(string connString) : base(connString)
        {
        }

        public DbSet<DemoEntity> Demo { get; set; }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEntity>().ToTable("Demo")
                .HasKey(e => e.Id)
                .Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            base.OnModelCreating(modelBuilder);
        }
    }
}
