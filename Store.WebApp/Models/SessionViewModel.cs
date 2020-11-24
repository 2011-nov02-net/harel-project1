using System;
using System.Collections.Generic;
using System.Linq;
using Store;
using Store.DataModel;

namespace Store.WebApp.Models
{
    public class SessionViewModel : ISession
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public List<Order> orders;
        public List<Customer> customers;
        public List<Location> locations;
        public List<Item> items;

        public IQueryable<IOrder> Orders => orders.AsQueryable();

        public IQueryable<ICustomer> Customers => throw new NotImplementedException();

        public IQueryable<ILocation> Locations => locations.AsQueryable();

        public IQueryable<IItem> Items => items.AsQueryable();

        public void AddCustomer(string name)
        {
            customers.Add(new Customer{ Name = name });
        }

        public void AddItem(string name)
        {
            items.Add(new Item { Name = name });
        }

        public void AddLocation(string name, Dictionary<int, int> itemCounts)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IOrder> OrderHistory(ILocation location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IOrder> OrderHistory(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}