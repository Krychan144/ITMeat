using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Order
{
    public class ActiveOrderViewModel
    {
        public Guid Id { get; set; }

        public string PubName { get; set; }

        public string CreatedOn { get; set; }

        public string EndDateTime { get; set; }

        public Guid PubId { get; set; }

        public Guid OwnerId { get; set; }
    }
}