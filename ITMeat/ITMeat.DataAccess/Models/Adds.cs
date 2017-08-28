using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITMeat.DataAccess.Models
{
    public class Adds : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(16,2)")]
        public decimal Expense { get; set; }

        [Required]
        public Pub Pub { get; set; }
    }
}