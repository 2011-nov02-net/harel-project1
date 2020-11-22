using System;
using System.Collections.Generic;

namespace Store
{
    public interface IOrder
    {
        int Id {get; set;}
        DateTime Placed {get; set;}
        int CustomerId {get; set;}
        int LocationId {get; set;}
        Dictionary<int, int> ItemCounts { get; set;}
    }
}