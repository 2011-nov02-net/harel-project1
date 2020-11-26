using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Store.WebApp.Models
{
    class LocationModel : ILocation
    {
        private int id;
        private string name;
        readonly List<ItemModel> items;
        readonly List<int> itemCounts;

        public LocationModel(ILocation location, IQueryable<IItem> allItems)
        {
            id = location.Id;
            name = location.Name;
            items = new List<ItemModel>();
            itemCounts = new List<int>();
            foreach (var kv in location.ItemCounts)
            {
                items.Add(new ItemModel(allItems
                    .Where(item => item.Id == kv.Key).First()));
                itemCounts.Add(kv.Value);
            }
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value;}
        public Dictionary<int, int> ItemCounts
        {
            get
            {
                var d = new Dictionary<int,int>();
                for (int i = 0; i < items.Count; i++) d.TryAdd(items[i].Id, itemCounts[i]);
                return d;
            }
        }
    }
}