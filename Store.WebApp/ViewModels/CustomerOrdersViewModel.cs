using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Store.WebApp.Models
{
    public class CustomerOrdersViewModel
    {
        public IEnumerable<IOrder> Orders {get; set;}
        public IEnumerable<ItemModel> Items {get; set;}
        public CustomerModel Customer {get; set;}
        public CustomerOrdersViewModel(
            CustomerModel customer,
            IEnumerable<ItemModel> items,
            IEnumerable<IOrder> orders)
        {
            Customer = customer;
            Items = items;
            Orders = orders;
        }
    }
}