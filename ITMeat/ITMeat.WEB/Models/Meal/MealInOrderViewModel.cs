using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.DataAccess.Models;

namespace ITMeat.WEB.Models.Meal
{
    public class MealInOrderViewModel
    {
        public Guid Userid { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public decimal Expense { get; set; }

        public string Type { get; set; }
    }
}