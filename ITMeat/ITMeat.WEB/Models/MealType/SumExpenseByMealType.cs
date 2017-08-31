using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.MealType
{
    public class SumExpenseByMealType
    {
        public string MealTypeName { get; set; }

        public decimal SumExpense { get; set; }
    }
}