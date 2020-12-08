using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Store;

namespace Store.Domain
{
    public class LocationA : ILocation
    {
        public LocationA(int id) {
            this.id = id;
            ItemCounts = new Dictionary<int, int>();
        }
        private int id;
        public int Id {get => id; set => id = value;}
        private string name;
        public string Name {get => name; set => name = value;}
        private Dictionary<int, int> itemCounts;
        public Dictionary<string, int> Inventory
        {
            get
            {
                var result = new Dictionary<string, int>();
                foreach (var kv in itemCounts)
                {
                    result.Add(kv.Key.ToString(), kv.Value);
                }
                return result;
            }
            set
            {
                var result = new Dictionary<int, int>();
                foreach (var kv in value) 
                {
                    result.Add(Convert.ToInt32(kv.Key), kv.Value);
                }
                itemCounts = result;
            }
        }
        [JsonIgnore]
        public Dictionary<int, int> ItemCounts 
        {
            get => itemCounts; 
            set => itemCounts = value; 
        }
    }
}