using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.Models
{
    public class CustomerModel : ICustomer
    {
        
        private int _id;
        private string _name;
        public int Id { get => _id; set => _id = value; }
        [Display(Name = "Customer Name")]
        [MaxLength(80)]
        public string Name { get => _name; set => _name = value; }
    }
}