using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITMeat.DataAccess.Models
{
    public class MealExpenseSum
    {
        public string ItemName { get; set; }

        [Column(TypeName = "DECIMAL(16,2)")]
        public decimal Expense { get; set; }
    }
}