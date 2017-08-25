using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.MealType
{
    public class MealTypeToEditViewModel
    {
        public Guid MealTypeId { get; set; }

        public string MealtypeName { get; set; }
    }
}