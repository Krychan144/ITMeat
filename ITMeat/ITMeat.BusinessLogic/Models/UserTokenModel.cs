using System;

namespace ITMeat.BusinessLogic.Models
{
    public class UserTokenModel : BaseModel
    {
        public UserModel User { get; set; }

        public string SecretToken { get; set; }

        public DateTime? SecretTokenTimeStamp { get; set; }
    }
}