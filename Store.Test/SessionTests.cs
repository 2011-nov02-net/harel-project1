using System;
using System.Linq;
using System.Collections.Generic;
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
            context.Database.EnsureCreated();
            var item1 = new Item { Name = "Item1"};
            var item2 = new Item { Name = "Item2"};
            context.Items.Add(item1);
            context.Items.Add(item2);
            
            var location1 = new Location { Name = "Location1"};
            context.Locations.Add(location1);
            location1.LocationItems.Add(new LocationItem {
                Item = item1,
                ItemCount = 21
            });
            location1.LocationItems.Add(new LocationItem {
                Item = item2,
                ItemCount = 8
            });
            var location2 = new Location { Name = "Location2"};
            context.Locations.Add(location2);
            location2.LocationItems.Add(new LocationItem {
                Item = item1,
                ItemCount = 5
            });
            context.Customers.Add(new Customer { Name = "Customer1" });
            context.Customers.Add(new Customer { Name = "Customer2" });
            context.SaveChanges();
        }
        [Theory]
        [InlineData("Name1")]
        [InlineData("Har'el Fishbein")]
        [InlineData(" #$#ewje3a3 #(d3+_ ")]
        public void TestAddCustomer(string name)
        {
            using var context = new Project1Context(_options);
            var session = new Repository(context);
            session.AddCustomer(name);
            Assert.Contains(name, session.Customers.Select(x => x.Name));
        }
        [Theory]
        [InlineData("Name1")]
        public void TestAddItem(string name)
        {
        //Given
        using var context = new Project1Context(_options);
        var session = new Repository(context);
        //When
        session.AddItem(name);
        //Then
        Assert.Contains(name, session.Items.Select(x => x.Name));
        }
        // session.OrderHistory
        [Fact]
        public void TestAddLocation()
        {
            using var context = new Project1Context(_options);
            var session = new Repository(context);
            int i_id = session.Items.First().Id;
            int i2_id = session.Items.Last().Id;
            var testd = new Dictionary<int, int> { [i_id] = 10, [i2_id] = 21 };
            session.AddLocation(" #$#ewje3a3 #(d3+_ ", testd);
            Assert.Contains(" #$#ewje3a3 #(d3+_ ", session.Locations.Select(x => x.Name));
            Assert.Contains(testd, session.Locations.Select(x => x.ItemCounts));
        }
        [Fact]
        public void TestAddOrder()
        {
            using var context = new Project1Context(_options);
            var session = new Repository(context);
            var testd = new Dictionary<int, int> { [session.Items.First().Id] = 3 };
            session.AddOrder(
                session.Customers.First(),
                session.Locations.First(),
                testd
            );
            Assert.Contains(testd, session.Orders.Select(x => x.ItemCounts));
        }
        [Fact]
        public void TestOrderMaxItemsRule()
        {
            using var context = new Project1Context(_options);
            var session = new Repository(context);
            var testd = new Dictionary<int, int> { [session.Items.First().Id] = 100 };
            Assert.Throws<DbUpdateException>(new Action(() => 
                session.AddOrder(
                    session.Customers.First(),
                    session.Locations.First(),
                    testd
            )));
            Assert.DoesNotContain(testd, session.Orders.Select(x => x.ItemCounts));
        }
        [Fact]
        public void TestOrderInventoryRule()
        {
            using var context = new Project1Context(_options);
            var session = new Repository(context);
            var testd = new Dictionary<int, int> { [session.Items.First(x => x.Name == "Item1").Id] = 6 };
            Assert.ThrowsAny<Exception>(new Action(() => 
                session.AddOrder(
                    session.Customers.First(),
                    session.Locations.First(x => x.Name == "Location2"),
                    testd
            )));
            Assert.DoesNotContain(testd, session.Orders.Select(x => x.ItemCounts));
        }
    }
}
