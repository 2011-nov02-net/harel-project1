using System.ComponentModel.DataAnnotations;

namespace Store.DataModel
{
    public partial class Item
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        [Required]
        public string Name {get; set;}
    }
}