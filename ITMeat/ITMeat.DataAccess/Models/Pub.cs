using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class Pub : BaseEntity
    {
        public Pub()
        {
            Meals = new HashSet<Meal>();
            PubOrders = new HashSet<PubOrder>();
        }

        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<PubOrder> PubOrders { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal FreeDelivery { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(11,0)")]
        public decimal Phone { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Adress { get; set; }
    }
}