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
        public int LocationId {get;set;}
        [Key, Column(Order = 1), ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int ItemId;
        [Key, Column(Order = 2), ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int ItemCount { get => itemCount; set => itemCount = value; }
    }
}