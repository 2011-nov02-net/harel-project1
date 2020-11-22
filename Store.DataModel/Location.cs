using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store;

namespace Store.DataModel
{
    public partial class Location
    {
        private string name;
        private int id;

        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }
        [Required]
        [MaxLength(80)]
        public string Name { get => name; set => name = value; }
        public virtual ICollection<LocationItem> LocationItems { get; set; }
    }
    public partial class LocationItem
    {
        private int itemCount;

        [Key]
        [Column(Order = 1)]
        public virtual Location Location { get; set; }
        [Key]
        [Column(Order = 2)]
        public virtual Item Item { get; set; }
        [Range(1, int.MaxValue)]
        public int ItemCount { get => itemCount; set => itemCount = value; }
    }
}