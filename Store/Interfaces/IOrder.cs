using System;
using System.Collections.Generic;

namespace Store
{
    public interface IOrder
    {
        int Id {get;}
        DateTime Placed {get; set;}
        int CustomerId {get;}
        int LocationId {get;}
        Dictionary<int, int> ItemCounts { get;}
    }
}