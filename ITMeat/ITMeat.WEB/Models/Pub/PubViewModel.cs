using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Models;

namespace ITMeat.WEB.Models.Pub
{
    public class PubViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }
    }
}