using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class ConfirmUserEmailByToken : IConfirmUserEmailByToken
    {
        private readonly IUserRepository userRepository;
        private readonly IUserTokenRepository userTokenRepository;

        public ConfirmUserEmailByToken(IUserTokenRepository userTokenRepository, IUserRepository userRepository)
        {
            this.userTokenRepository = userTokenRepository;
            this.userRepository = userRepository;
        }

        public bool Invoke(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return false;
            }

            var userToken = userTokenRepository.FindBy(x => x.SecretToken == guid).FirstOrDefault();

            if (userToken == null
                || userToken.User.EmailConfirmedOn != null
                || userToken.SecretTokenTimeStamp <= DateTime.UtcNow)
            {
                return false;
            }

            userToken.User.EmailConfirmedOn = DateTime.UtcNow;

            userRepository.Edit(userToken.User);
            userRepository.Save();

            userTokenRepository.Delete(userToken);
            userTokenRepository.Save();

            return true;
        }
    }
}