namespace Store.Domain
{
    public class ItemA : IItem
    {
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        private int id;
        private string name;
        public ItemA(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}