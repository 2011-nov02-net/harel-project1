using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataModel
{
    public partial class Order
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Placed {get;set;}
        public virtual Customer Customer { get; set;}
        public virtual Location Location { get; set;}
        public virtual ICollection<OrderItem> OrderItems { get; set;}
    }
    public partial class OrderItem
    {
        [Key]
        [Column(Order=1)]
        public virtual Order Order { get; set;}
        [Key]
        [Column(Order=2)]
        public virtual Item Item { get; set;}
        [Range(1, 20)]
        public int ItemCount { get; set;}
    }
}