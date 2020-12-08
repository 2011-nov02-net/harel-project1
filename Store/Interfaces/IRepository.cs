using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public interface IRepository
    {
        IQueryable<IOrder> Orders {get;}
        IQueryable<ICustomer> Customers {get;}
        IQueryable<ILocation> Locations {get;}
        IQueryable<IItem> Items {get;}
        IItem AddItem(string name);
        ICustomer AddCustomer(string name);
        ILocation AddLocation(string name, Dictionary<int, int> itemCounts);
        IOrder AddOrder(ICustomer customer, 
                      ILocation location, 
                      Dictionary<int, int> itemCounts);
    }
}