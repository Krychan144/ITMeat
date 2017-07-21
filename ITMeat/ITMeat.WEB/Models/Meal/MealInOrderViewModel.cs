using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.DataAccess.Models;

namespace ITMeat.WEB.Models.Meal
{
    public class MealInOrderViewModel
    {
        public UserOrder UserOrder { get; set; }

        public string Name { get; set; }

        public decimal Expense { get; set; }

        public string Type { get; set; }
    }
}