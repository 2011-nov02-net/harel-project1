using System;
using System.Linq;
using Xunit;
using Store.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Store.Test
{
    public class SessionTests
    {
        private readonly DbContextOptions<Project1Context> _options;
        private const string connectionString = "Data Source=../XUnit.db";
        public SessionTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
            optionsBuilder.UseSqlite(connectionString);
            _options = optionsBuilder.Options;
            populate();
        }
        internal void populate()
        {
            using var context = new Project1Context(_options);
            var item1 = new Item { Name = "Item1"};
            var item2 = new Item { Name = "Item2"};
            context.Items.Add(item1);
            context.Items.Add(item2);
            var location2 = new Location { Name = "Location2"};
            var location1 = new Location { Name = "Location1"};
            context.Locations.Add(location1);
            context.Locations.Add(location2);
            context.LocationItem.Add(new LocationItem {
                Item = item1,
                Location = location1,
                ItemCount = 21
            });
            context.LocationItem.Add(new LocationItem {
                Item = item2,
                Location = location1,
                ItemCount = 8
            });
            context.LocationItem.Add(new LocationItem {
                Item = item1,
                Location = location2,
                ItemCount = 5
            });
            context.SaveChanges();
        }
        [Theory]
        [InlineData("Name1")]
        [InlineData("Har'el Fishbein")]
        [InlineData(" #$#ewje3a3 #(d3+_ ")]
        public void TestAddCustomer(string name)
        {
            using var context = new Project1Context(_options);
            var session = new Session(context);
            session.AddCustomer(name);
            Assert.Contains(name, session.Customers.Select(x => x.Name));
        }
        [Theory]
        [InlineData("Name1")]
        public void TestAddItem(string name)
        {
        //Given
        using var context = new Project1Context(_options);
        var session = new Session(context);
        //When
        session.AddItem(name);
        //Then
        Assert.Contains(name, session.Items.Select(x => x.Name));
        }
        // session.OrderHistory
    }
}
