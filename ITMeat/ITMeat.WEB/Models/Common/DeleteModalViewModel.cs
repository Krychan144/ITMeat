using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.Common
{
    public class DeleteModalViewModel
    {
        public string ControllerName { get; set; }

        public string Action { get; set; }

        public string Text { get; set; }

        public Guid IdToDelete { get; set; }
    }
}