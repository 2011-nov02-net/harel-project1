using System.Collections.Generic;
using System.Linq;

namespace Store.WebApp.Models
{
    internal class AddOrderViewModel
    {
        public LocationModel Location {get; set;}
        public IEnumerable<CustomerModel> Customers { get; set; }
        public AddOrderViewModel(ILocation location, 
                                 IQueryable<ICustomer> customers, 
                                 IQueryable<IItem> items)
        {
            Location = new LocationModel(location, items);
            Customers = customers.Select(x => new CustomerModel(x));
        }
    }
}