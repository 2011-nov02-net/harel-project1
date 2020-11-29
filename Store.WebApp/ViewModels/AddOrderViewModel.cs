using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Store.DataModel;

namespace Store.WebApp.Models
{
    public class AddOrderViewModel
    {
        [HiddenInput]
        public LocationModel Location {get; set;}
        public IEnumerable<CustomerModel> Customers { get; set; }
        public CustomerModel Customer => Customers.Where(x => 
            x.Id == Convert.ToInt32(CustomerId)).FirstOrDefault();
        public List<SelectListItem> CustomersSelect => 
            Customers.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        public SelectList CustomersSelectList => new SelectList(CustomersSelect);
        [Required]
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public int CountMax {get; }
        public AddOrderViewModel(ILocation location, 
                                 IQueryable<ICustomer> customers, 
                                 IQueryable<IItem> items,
                                 int countMax = OrderItem.countMax)
        {
            Customers = customers.Select(x => new CustomerModel(x));
            Location = new LocationModel(location, items);
            LocationId = Location.Id;
            CountMax = countMax;
        }
    }
}