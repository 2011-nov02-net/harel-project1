using System;
using System.ComponentModel.DataAnnotations;

namespace Store.DataModel
{
    public partial class Item
    {
        private int id;
        private string name;

        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }
        [Required]
        [MaxLength(80)]
        public string Name { get => name; set => name = value; }
    }
}