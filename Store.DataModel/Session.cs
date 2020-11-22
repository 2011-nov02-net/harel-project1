using System;
using System.Collections.Generic;
using System.Linq;
using Store;

namespace Store.DataModel
{
    public class SessionA : ISession
    {
        public ISession session;
        public SessionA() 
        {
            Console.WriteLine("hello");
        }

        public IQueryable<IOrder> Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueryable<ICustomer> Customers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueryable<ILocation> Locations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueryable<IItem> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddCustomer(string name)
        {
            throw new NotImplementedException();
        }

        public void AddItem(string name)
        {
            throw new NotImplementedException();
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
    public partial class Order : IOrder
    {
        int IOrder.Id => this.Id;

        DateTime IOrder.Placed 
        { 
            get => this.Placed; 
            set => this.Placed = value; 
        }

        int IOrder.CustomerId => this.Customer.Id;

        int IOrder.LocationId => this.Location.Id;

        Dictionary<int, int> IOrder.ItemCounts
        {
            get 
            { 
                var orderItemsDict = new Dictionary<int,int>();
                foreach (var io in this.OrderItems) orderItemsDict.Add(io.Item.Id, io.ItemCount);
                return orderItemsDict;
            }
            set
            {
                this.OrderItems.Clear();
                foreach (var kv in value)
                {
                    this.OrderItems.Add(
                        new OrderItem {
                            ItemCount = kv.Value, 
                            Item = new Item { Id = kv.Key }
                            }
                        );
                }
            }
        }
    }
}