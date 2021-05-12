using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoubleTrouble
{
    public class DbContextCore : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContextCore(DbContextOptions<DbContextCore> options) : base(options)
        {
        }

        public DbSet<DemoEntity> Demo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoEntity>().ToTable("Demo")
                .HasKey(e => e.Id);
            modelBuilder.Entity<DemoEntity>()
                .Property(e => e.Id).HasDefaultValueSql();

            base.OnModelCreating(modelBuilder);
        }
    }
}
