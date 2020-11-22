using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public interface ISession
    {
        IQueryable<IOrder> Orders {get;}
        IQueryable<ICustomer> Customers {get;}
        IQueryable<ILocation> Locations {get;}
        IQueryable<IItem> Items {get;}
        void AddItem(string name);
        void AddCustomer(string name);
        void AddLocation(string name, Dictionary<int, int> itemCounts);
        void AddOrder(ICustomer customer, ILocation location, Dictionary<int, int> itemCounts);
        IEnumerable<IOrder> OrderHistory(ILocation location);
        IEnumerable<IOrder> OrderHistory(ICustomer customer);
    }
}