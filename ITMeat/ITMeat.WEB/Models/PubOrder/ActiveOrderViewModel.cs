using System;

namespace ITMeat.WEB.Models.PubOrder
{
    public class ActiveOrderViewModel
    {
        public Guid Id { get; set; }

        public string PubName { get; set; }

        public string CreatedOn { get; set; }

        public string EndDateTime { get; set; }

        public Guid PubId { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; }
    }
}