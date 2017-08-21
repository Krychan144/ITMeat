using System;
using System.Collections.Generic;
using System.Text;

namespace ITMeat.BusinessLogic.Models
{
    public class OrderModel : BaseModel
    {
        public decimal Expense { get; set; }

        public UserModel Owner { get; set; }

        public DateTime EndDateTime { get; set; }

        public string Name { get; set; }

        public DateTime? SubmitDateTime { get; set; }
    }
}