using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITMeat.DataAccess.Models
{
    public class UserOrderAdds : BaseEntity
    {
        [Required]
        public UserOrder UserOrder { get; set; }

        [Required]
        public Adds Adds { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}