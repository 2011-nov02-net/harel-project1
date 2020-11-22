using System;
using System.ComponentModel.DataAnnotations;
using Store;

namespace Store.DataModel
{
    public partial class Customer
    {
        private string name;
        private int id;

        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }
        [Required]
        [MaxLength(80)]
        public string Name { get => name; set => name = value; }
    }
}