using System;

namespace Store
{
    public interface IItem
    {
        int Id {get; set; }
        string Name {get; set;}
    }
}