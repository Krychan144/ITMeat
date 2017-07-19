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
            Meals = new HashSet<Meal>();
        }

        public virtual ICollection<Meal> Meals { get; set; }

        [Required]
        public User Owner { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal Expense { get; set; }

        [Required]
        public Guid PubId { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public DateTime SubmitOrderDate { get; set; }
    }
}