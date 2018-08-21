using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models;

namespace ShopApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<Photo> Photo {get;set;}

        public DbSet<SystemVariable> SystemVariable {get;set;}


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<House>()
        //         .HasOne(p => p.User)
        //         .WithMany(b => b.Houses);
        // }
    }
}