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
        private const string connectionString = "";
        public SessionTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
            optionsBuilder.UseSqlite(connectionString);
            _options = optionsBuilder.Options;
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
