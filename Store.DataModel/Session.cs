using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Store.DataModel
{
    public class Session : ISession
    {
        private readonly DbContextOptions<Project1Context> _options;
        public Session(DbContextOptions<Project1Context> options) {
            _options = options;
        }

        public IQueryable<IOrder> Orders
        {
            get
            {
                using var context = new Project1Context(_options);
                return context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Location)
                .Include(x => x.OrderItems).ThenInclude(x => x.Item)
                .AsQueryable();
            }
        }
        public IQueryable<ILocation> Locations 
        { 
            get 
            {
                using var context = new Project1Context(_options);
                return context.Locations
                .Include(x => x.LocationItems).ThenInclude(x => x.Item)
                .ToList().AsQueryable();
            }
        }
        public IQueryable<ICustomer> Customers 
        { 
            get 
            {
                using var context = new Project1Context(_options);
                return context.Customers.AsQueryable();
            }
        }
        public IQueryable<IItem> Items 
        { 
            get
            {
                using var context = new Project1Context(_options);
                return context.Items.AsQueryable();
            }
        }

        public void AddCustomer(string name)
        {
            using var context = new Project1Context(_options);
            context.Customers.Add(new Customer{ Name = name });
            context.SaveChanges();
        }

        public void AddItem(string name)
        {
            using var context = new Project1Context(_options);
            context.Items.Add(new Item { Name = name });
            context.SaveChanges();
        }

        public void AddLocation(string name, Dictionary<int, int> itemCounts)
        {
            using var context = new Project1Context(_options);
            var location = new Location { Name = name };
            context.Locations.Add(location);
            foreach (var kv in itemCounts)
            {
                context.LocationItems.Add(
                    new LocationItem 
                    { 
                        Location = location, 
                        Item = context.Items.Find(kv.Key), 
                        ItemCount = kv.Value }
                );
            }
            context.SaveChanges();
        }

        public void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts)
        {
            using var context = new Project1Context(_options);
            var order = new Order 
            {
                Customer = context.Customers.Find(customer.Id),  
                Location = context.Locations.Find(location.Id),
            };
            foreach (var kv in itemCounts)
            {
                context.OrderItems.Add(
                    new OrderItem
                    {
                        Order = order,
                        Item = context.Items.Find(kv.Key),
                        ItemCount = kv.Value
                    }
                );
            }
            context.SaveChanges();
        }

        public IEnumerable<IOrder> OrderHistory(ILocation location)
        {
            return Orders.Where(x => x.LocationId == location.Id).ToList();
        }

        public IEnumerable<IOrder> OrderHistory(ICustomer customer)
        {
            return Orders.Where(x => x.CustomerId == customer.Id).ToList();
        }
    }
    public partial class Item : IItem {}
    public partial class Customer : ICustomer { }
    public partial class Order : IOrder
    {
        int IOrder.CustomerId => this.Customer.Id;
        int IOrder.LocationId => this.Location.Id;
        Dictionary<int, int> IOrder.ItemCounts
        {
            get 
            { 
                var orderItemsDict = new Dictionary<int,int>();
                foreach (var io in OrderItems) orderItemsDict.Add(io.Item.Id, io.ItemCount);
                return orderItemsDict;
            }
        }
    }
    public partial class Location : ILocation
        {
            public Dictionary<int, int> ItemCounts 
            { 
                get
                { 
                    var locationItemsDict = new Dictionary<int,int>();
                    foreach (var io in LocationItems) locationItemsDict.Add(io.Item.Id, io.ItemCount);
                    return locationItemsDict;
                } 
            }
        }
}