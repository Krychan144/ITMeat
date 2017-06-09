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
        }

        public virtual ICollection<Meal> Meals { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Adress { get; set; }
    }
}