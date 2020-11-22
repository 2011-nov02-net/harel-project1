using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public interface ISession
    {
        IQueryable<IOrder> Orders {get; set;}
        IQueryable<ICustomer> Customers {get; set;}
        IQueryable<ILocation> Locations {get; set;}
        IQueryable<IItem> Items {get; set;}
        void AddItem(string name);
        void AddCustomer(string name);
        void AddLocation(string name, Dictionary<int, int> itemCounts);
        void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts);
        IEnumerable<IOrder> OrderHistory(ILocation location);
        IEnumerable<IOrder> OrderHistory(ICustomer customer);
    }
}