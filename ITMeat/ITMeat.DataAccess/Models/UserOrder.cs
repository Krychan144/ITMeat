using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class UserOrder : BaseEntity
    {
        public UserOrder()
        {
            OrdersMeals = new HashSet<UserOrderMeal>();
            OrdersAdds = new HashSet<UserOrderAdds>();
        }

        public virtual ICollection<UserOrderMeal> OrdersMeals { get; set; }
        public virtual ICollection<UserOrderAdds> OrdersAdds { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal Expense { get; set; }
    }
}