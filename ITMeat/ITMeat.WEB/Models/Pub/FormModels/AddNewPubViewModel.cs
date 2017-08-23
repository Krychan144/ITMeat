using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Pub.FormModels
{
    public class AddNewPubViewModel
    {
        public string Name { get; set; }

        public decimal FreeDelivery { get; set; }

        public decimal Phone { get; set; }

        public string Adress { get; set; }
    }
}