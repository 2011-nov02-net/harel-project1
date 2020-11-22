using System;
using Microsoft.EntityFrameworkCore;

namespace Store.DataModel
{
    public class Project1Context : DbContext 
    {
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Location> Locations { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<Item> Items { get; set;}
        internal DbSet<OrderItem> OrderItems { get; set; }
        internal DbSet<LocationItem> LocationItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<LocationItem>(entity =>
            {
                entity.HasCheckConstraint(
                    name: "CK_Location_Inventory_Positive",
                    sql: "[ItemCount] > 0"
                );
            });
            modelBuilder.Entity<OrderItem>(entity => 
            {
                entity.HasCheckConstraint(
                    name: "CK_Order_MaxInventory",
                    sql: $"[ItemCount] <= {OrderItem.countMax}"
                );
                entity.HasCheckConstraint(
                    name: "CK_Order_Inventory_Positive",
                    sql: "[ItemCount] > 0"
                );
            });
        }
        public Project1Context(DbContextOptions<Project1Context> options) : base(options) { }
    }
}
