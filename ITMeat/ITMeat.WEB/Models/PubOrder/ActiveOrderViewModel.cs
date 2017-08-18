using System;

namespace ITMeat.WEB.Models.PubOrder
{
    public class ActiveOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string PubName { get; set; }

        public string CreatedOn { get; set; }

        public string EndDateTime { get; set; }

        public Double EndDateTimeData { get; set; }

        public bool ToSubmitet { get; set; }

        public Guid PubId { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; }

        public Guid PubOrderId { get; set; }

        public bool IsJoined { get; set; }
    }
}