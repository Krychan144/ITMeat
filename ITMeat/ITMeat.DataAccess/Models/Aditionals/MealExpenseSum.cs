using System.ComponentModel.DataAnnotations.Schema;

namespace ITMeat.DataAccess.Models.Aditionals
{
    public class MealExpenseSum
    {
        public string ItemName { get; set; }

        [Column(TypeName = "DECIMAL(16,2)")]
        public decimal Expense { get; set; }
    }
}