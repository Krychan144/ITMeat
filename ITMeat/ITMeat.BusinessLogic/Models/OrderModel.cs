using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Models
{
    public class OrderModel : BaseModel
    {
        public List<MealModel> Meals { get; set; }

        public UserModel Owner { get; set; }

        public string Name { get; set; }

        public decimal Expense { get; set; }
    }
}