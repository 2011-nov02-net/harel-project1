using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.Models
{
    class AddOrderViewModel
    {
        [HiddenInput]
        public LocationModel Location {get; set;}
        public IEnumerable<CustomerModel> Customers { get; set; }
        public CustomerModel Customer => Customers.Where(x => 
            x.Id == Convert.ToInt32(CustomerId)).FirstOrDefault();
        public List<SelectListItem> CustomersSelect => 
            Customers.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        [Required]
        public string CustomerId { get; set; }
        public string LocationId { get; set; }
        public AddOrderViewModel(ILocation location, 
                                 IQueryable<ICustomer> customers, 
                                 IQueryable<IItem> items)
        {
            Customers = customers.Select(x => new CustomerModel(x));
            Location = new LocationModel(location, items);
            LocationId = Location.Id.ToString();
        }
    }
}