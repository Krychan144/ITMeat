using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Meal
{
    public class AddNewMealToOrderView
    {
        public Guid PubId { get; set; }

        public Guid User { get; set; }

        public byte Quantity { get; set; }

        public string MealName { get; set; }

        public Decimal Expanse { get; set; }
    }
}