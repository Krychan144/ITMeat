using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITMeat.WEB.Models.Pub
{
    public class SelectPubToEditViewModel
    {
        public Guid PubId { get; set; }

        public List<SelectListItem> Pubs { get; set; }
    }
}