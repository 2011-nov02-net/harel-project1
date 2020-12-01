using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Store.WebApp.Models
{
    public class LocationModel : ILocation
    {
        private int id;
        private string name;
        public readonly List<ItemModel> items;
        public readonly List<int> itemCounts;
        public LocationModel(ILocation location)
        {
            id = location.Id;
            name = location.Name;
        }
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
        [Display(Name = "Location Id")]
        public int Id { get => id; set => id = value; }
        //[DisplayFormat()]
        [Display(Name = "Location Name")]
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