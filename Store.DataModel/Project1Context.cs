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
    }
}
