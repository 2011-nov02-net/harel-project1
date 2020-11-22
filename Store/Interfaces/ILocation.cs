using System;
using System.Collections.Generic;

namespace Store
{
    public interface ILocation
    {
        int Id {get; set;}
        string Name {get; set;}
        Dictionary<int, int> ItemCounts {get;}
    }
}