using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class Meal : BaseEntity
    {
        [Required]
        public UserOrder UserOrder { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16,2)")]
        public decimal Expense { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Type { get; set; }
    }
}