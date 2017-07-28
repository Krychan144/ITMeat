using System.ComponentModel.DataAnnotations;

namespace ITMeat.DataAccess.Models
{
    public class UserOrderMeal : BaseEntity
    {
        [Required]
        public UserOrder UserOrder { get; set; }

        [Required]
        public Meal Meal { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}