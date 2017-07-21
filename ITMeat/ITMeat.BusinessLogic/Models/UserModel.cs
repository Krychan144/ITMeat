using System;
using System.Collections.Generic;

namespace ITMeat.BusinessLogic.Models
{
    public class UserModel : BaseModel
    {
        public List<UserOrderModel> UserOrders { get; set; }

        public List<UserTokenModel> Tokens { get; set; }

        public string Email { get; set; }

        public DateTime? EmailConfirmedOn { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public DateTime? LockedOn { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return !string.IsNullOrEmpty(Email.Trim()) && !string.IsNullOrEmpty(Name.Trim());
        }
    }
}