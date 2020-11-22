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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Placed { get => placed; set => placed = value; }
        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
    public partial class OrderItem
    {
        private int itemCount;
        public const int countMax = 20;

        [Key]
        [Column(Order = 1)]
        public virtual Order Order { get; set; }
        [Key]
        [Column(Order = 2)]
        public virtual Item Item { get; set; }
        [Range(1, countMax)]
        public int ItemCount { get => itemCount; set => itemCount = value; }
    }
}