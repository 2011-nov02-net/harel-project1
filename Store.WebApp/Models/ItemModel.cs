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
        int id;
        string name;

        public int Id 
        { 
            get => id; 
            set => id = value; 
        }
        public string Name 
        { 
            get => name; 
            set => name = value; 
        }
    }
}