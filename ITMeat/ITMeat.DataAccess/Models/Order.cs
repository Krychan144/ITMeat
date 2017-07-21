using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrdersMeals = new HashSet<OrderMeal>();
        }

        public virtual ICollection<OrderMeal> OrdersMeals { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal Expense { get; set; }

        public Guid PubOrderId { get; set; }
    }
}