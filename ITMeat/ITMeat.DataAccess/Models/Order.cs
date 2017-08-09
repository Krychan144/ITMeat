using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models
{
    public class Order : BaseEntity
    {
        [Required]
        [Column(TypeName = "DECIMAL(16 ,2)")]
        public decimal Expense { get; set; }

        [Required]
        public User Owner { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        public DateTime? SubmitDateTime { get; set; }
    }
}