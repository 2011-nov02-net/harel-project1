using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Store.WebApp.Models
{
    public class LocationOrdersViewModel
    {
        public IEnumerable<IOrder> Orders {get; set;}
        public IEnumerable<ItemModel> Items {get; set;}
        public LocationModel Location {get; set;}
        public LocationOrdersViewModel (
            LocationModel location, 
            IEnumerable<ItemModel> items,
            IEnumerable<IOrder> orders)
        {
            Location = location;
            Items = items;
            Orders = orders;
        }
    }
}