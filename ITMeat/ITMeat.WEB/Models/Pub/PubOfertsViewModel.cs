using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Pub
{
    public class PubOfertsViewModel
    {
        public Guid MealId { get; set; }

        public string MealName { get; set; }

        public decimal MealExpense { get; set; }

        public Guid MealTypeId { get; set; }

        public string MealTypeName { get; set; }

        public Guid PubId { get; set; }

        public string PubName { get; set; }
    }
}