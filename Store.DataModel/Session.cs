using System;
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
                .AsQueryable();
            }
        }
        public IQueryable<ILocation> Locations
        {
            get
            {
                return _context.Locations
                .Include(x => x.LocationItems).ThenInclude(x => x.Item)
                .AsQueryable();
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
                        ItemCount = kv.Value 
                    }
                );
            }
            _context.SaveChanges();
        }
        public void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts)
        {
            var myLocation = _context.Locations.Find(location.Id);
            var order = new Order
            {
                Customer = _context.Customers.Find(customer.Id),
                Location = myLocation,
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
                var itemL = myLocation.LocationItems.First(li => li.ItemId == kv.Key);
                itemL.ItemCount -= kv.Value;
                if (itemL.ItemCount == 0) _context.LocationItems.Remove(itemL);
            }
            _context.SaveChanges();
        }
    }
    public partial class Item : IItem {}
    public partial class Customer : ICustomer {}
    public partial class Order : IOrder
    {
        public override string ToString()
        {
            return $"Location: {Location.Name}, Customer:{Customer.Name}\n" + 
                $"Id: {Id}, {Placed}\n" + String.Join(", ",
                OrderItems.Select(x => $"{x.Item.Name}: {x.ItemCount}").ToList() );
        }
        [NotMapped]
        Dictionary<int, int> IOrder.ItemCounts
        {
            get
            {
                var orderItemsDict = new Dictionary<int,int>();
                foreach (var io in OrderItems) orderItemsDict.Add(io.Item.Id, io.ItemCount);
                return orderItemsDict;
            }
        }
        [NotMapped]
        IEnumerable<IItem> IOrder.Items => OrderItems.Select(oi => oi.Item);
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
                    foreach (var lo in LocationItems) locationItemsDict.Add(lo.Item.Id, lo.ItemCount);
                    return locationItemsDict;
                }
            }
        }
}
