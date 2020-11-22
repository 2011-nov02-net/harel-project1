using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store;

namespace Store.DataModel
{
    public partial class Location
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        [Required]
        public string Name {get; set;}
        public virtual ICollection<LocationItem> LocationItems {get;set;}
    }
    public partial class LocationItem
    {
        [Key]
        [Column(Order=1)]
        public virtual Location Location {get;set;}
        [Key]
        [Column(Order=2)]
        public virtual Item Item {get;set;}
        [Range(1, int.MaxValue)]
        public int ItemCount {get; set;}
    }
}