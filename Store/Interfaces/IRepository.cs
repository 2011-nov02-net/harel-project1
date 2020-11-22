using System;
using System.Collections.Generic;

namespace Store
{
    public interface IRepository<T> : IDisposable, ICollection<T>
    {
    }
}