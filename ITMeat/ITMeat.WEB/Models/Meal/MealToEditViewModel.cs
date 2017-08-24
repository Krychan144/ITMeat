using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Models.Meal
{
    public class MealToEditViewModel
    {
        public Guid MealId { get; set; }

        public string MealName { get; set; }

        public decimal MealExpense { get; set; }

        public Guid MealTypeId { get; set; }

        public List<SelectListItem> MealTypes { get; set; }
    }
}