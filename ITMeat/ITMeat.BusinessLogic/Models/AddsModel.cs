using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Models
{
    public class AddsModel : BaseModel
    {
        public string Name { get; set; }

        public decimal Expense { get; set; }

        public PubModel Pub { get; set; }
    }
}