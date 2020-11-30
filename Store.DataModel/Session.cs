using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataModel
{
    public class Session : ISession
    {
        private readonly Project1Context _context;

        public Session(Project1Context context) {
            _context = context;
        }

        public IQueryable<IOrder> Orders
        {
            get
            {
                return _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.Location)
                .Include(x => x.OrderItems).ThenInclude(x => x.Item)
                .ToList().AsQueryable();
            }
        }
        public IQueryable<ILocation> Locations
        {
            get
            {
                return _context.Locations
                .Include(x => x.LocationItems).ThenInclude(x => x.Item)
                .ToList().AsQueryable();
            }
        }
        public IQueryable<ICustomer> Customers
        {
            get =>  _context.Customers.AsQueryable();
        }
        public IQueryable<IItem> Items
        {
            get => _context.Items.AsQueryable();
        }

        public void AddCustomer(string name)
        {
            _context.Customers.Add(new Customer{ Name = name });
            _context.SaveChanges();
        }
        public void AddItem(string name)
        {
            _context.Items.Add(new Item { Name = name });
            _context.SaveChanges();
        }

        public void AddLocation(string name, Dictionary<int, int> itemCounts)
        {
            var location = new Location { Name = name };
            _context.Locations.Add(location);
            foreach (var kv in itemCounts)
            {
                _context.LocationItems.Add(
                    new LocationItem
                    {
                        Location = location,
                        Item = _context.Items.Find(kv.Key),
                        ItemCount = kv.Value }
                );
            }
            _context.SaveChanges();
        }

        public void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts)
        {
            var order = new Order
            {
                Customer = _context.Customers.Find(customer.Id),
                Location = _context.Locations.Find(location.Id),
            };
            _context.Orders.Add(order);
            foreach (var kv in itemCounts)
            {
                _context.OrderItems.Add(
                    new OrderItem
                    {
                        Order = order,
                        Item = _context.Items.Find(kv.Key),
                        ItemCount = kv.Value
                    }
                );
            }
            _context.SaveChanges();
        }

        public IEnumerable<IOrder> OrderHistory(ILocation location)
        {
            return _context.Orders.Where(o => o.LocationId == location.Id).ToList();
        }

        public IEnumerable<IOrder> OrderHistory(ICustomer customer)
        {
            return _context.Orders.Where(o => o.CustomerId == customer.Id).ToList();
        }
    }
    public partial class Item : IItem {}
    public partial class Customer : ICustomer { }
    public partial class Order : IOrder
    {
        [NotMapped]
        Dictionary<int, int> IOrder.ItemCounts
        {
            get
            {
                var orderItemsDict = new Dictionary<int,int>();
                foreach (var io in OrderItems) 
                {
                    orderItemsDict.Add(io.Item.Id, io.ItemCount);
                }
                return orderItemsDict;
            }
        }
        [NotMapped]
        IEnumerable<IItem> IOrder.Items => OrderItems.Select(oi => oi.Item).ToList();
        ICustomer IOrder.Customer => Customer;  
        ILocation IOrder.Location => Location;
    }
    public partial class Location : ILocation
        {
            [NotMapped]
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
