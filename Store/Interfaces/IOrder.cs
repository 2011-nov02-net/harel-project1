using System;
using System.Collections.Generic;

namespace Store
{
    public interface IOrder
    {
        int Id {get;}
        DateTime Placed {get; set;}
        int CustomerId => Customer.Id;
        ICustomer Customer {get;}
        int LocationId => Location.Id;
        ILocation Location {get;}
        Dictionary<int, int> ItemCounts { get;}
        IEnumerable<IItem> Items {get;}
    }
}