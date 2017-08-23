using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Pub
{
    public class PubInfoViewModel
    {
        public Guid PubId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public decimal Phone { get; set; }
        public decimal FreeDelivery { get; set; }
    }
}