using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Order
{
    public class GetSubmitedOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string PubName { get; set; }

        public string CreatedOn { get; set; }

        public string EndDateTime { get; set; }

        public Guid PubId { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; }

        public Guid PubOrderId { get; set; }

        public decimal Expense { get; set; }
    }
}