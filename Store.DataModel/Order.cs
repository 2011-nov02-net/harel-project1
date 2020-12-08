using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataModel
{
    public partial class Order
    {
        private int id;
        private DateTime placed;

        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Placed { get => placed; set => placed = value; }
        public int CustomerId {get;set;}
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public int LocationId {get;set;}
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    public partial class OrderItem
    {
        private int itemCount;
        public const int countMax = 20;
        public int OrderId {get; set;}
        [Key, Column(Order = 1), ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public int ItemId {get;set;}
        [Key, Column(Order = 2), ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        [Range(1, countMax)]
        public int ItemCount { get => itemCount; set => itemCount = value; }
    }
}