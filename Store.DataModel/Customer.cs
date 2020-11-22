using System.ComponentModel.DataAnnotations;
using Store;

namespace Store.DataModel
{
    public partial class Customer : ICustomer
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        [Required]
        public string Name {get; set;}
    }
}