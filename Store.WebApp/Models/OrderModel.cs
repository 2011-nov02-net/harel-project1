using System;
using System.Collections.Generic;

namespace Store.WebApp.Models
{
    public class OrderModel : IOrder
    {
        private int _id;
        public int Id => _id;
        private int _customerId;
        private int _locationId;
        public DateTime Placed { get; set; }
        public int CustomerId { get => _customerId; set => _customerId = value; }
        public int LocationId { get => _locationId; set => _locationId = value; }
        private Dictionary<int, int> _itemCounts;
        public Dictionary<int, int> ItemCounts { get => _itemCounts; set => _itemCounts = value; }
    }
}