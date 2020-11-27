using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.Models
{
    public class ItemModel : IItem
    {
        public ItemModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public ItemModel(IItem item)
        {
            Id = item.Id;
            Name = item.Name;
        }
        private int id;
        private string name;
        public int Id 
        { 
            get => id; 
            set => id = value; 
        }
        [Display(Name = "Item Name")]
        [DisplayFormat()]
        public string Name 
        { 
            get => name; 
            set => name = value; 
        }
    }
}