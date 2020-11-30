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
                entity.ToTable("Customers");
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Items");
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Locations");
                entity.Property(e => e.Name).HasMaxLength(80);
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.Property(t => t.Placed).HasComputedColumnSql("GETDATE()");
                entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);
                entity.HasOne(d => d.Location).WithMany(p => p.Orders).HasForeignKey(d => d.LocationId);
            });
            modelBuilder.Entity<LocationItem>(entity =>
            {
                entity.ToTable("LocationItems");
                entity.HasKey(c => new { c.LocationId, c.ItemId });
                
                entity.HasCheckConstraint(
                    name: "CK_Location_Inventory_Positive",
                    sql: "[ItemCount] > 0"
                );
            });
            modelBuilder.Entity<OrderItem>(entity => 
            {
                entity.ToTable("OrderItems");
                entity.HasKey(c => new { c.OrderId, c.ItemId });
                entity.HasCheckConstraint(
                    name: "CK_Order_MaxInventory",
                    sql: $"[ItemCount] <= {OrderItem.countMax}"
                );
                entity.HasCheckConstraint(
                    name: "CK_Order_Inventory_Positive",
                    sql: "[ItemCount] > 0"
                );
                entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasForeignKey(d => d.ItemId);
            });
        }
        public Project1Context(DbContextOptions<Project1Context> options) : base(options) { }
    }
}
