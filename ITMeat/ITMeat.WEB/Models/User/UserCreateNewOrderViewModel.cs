using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ITMeat.WEB.Models.User
{
    public class UserCreateNewOrderViewModel
    {
        [Required]
        public string PubName { get; set; }

        [Required]
        public DateTime StartOrders { get; set; }

        [Required]
        public DateTime EndOrders { get; set; }
    }
}