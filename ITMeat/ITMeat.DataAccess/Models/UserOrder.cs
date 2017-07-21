using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class UserOrder : BaseEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal Expense { get; set; }
    }
}