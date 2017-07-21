using System.ComponentModel.DataAnnotations;

namespace ITMeat.DataAccess.Models
{
    public class OrderMeal : BaseEntity
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public Meal PubMeal { get; set; }
    }
}