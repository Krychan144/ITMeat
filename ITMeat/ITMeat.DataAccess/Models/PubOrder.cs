using System;
using System.ComponentModel.DataAnnotations;

namespace ITMeat.DataAccess.Models
{
    public class PubOrder : BaseEntity
    {
        [Required]
        public Order Order { get; set; }

        [Required]
        public Pub Pub { get; set; }

        [Required]
        public User Owner { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public DateTime SubmitDateTime { get; set; }
    }
}