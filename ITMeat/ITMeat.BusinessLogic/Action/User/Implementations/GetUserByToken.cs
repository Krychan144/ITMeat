using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class GetUserByToken : IGetUserByToken
    {
        private readonly IUserTokenRepository userTokenRepository;

        public GetUserByToken(IUserTokenRepository userTokenRepository)
        {
            this.userTokenRepository = userTokenRepository;
        }

        public UserModel Invoke(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var result = userTokenRepository.FindBy(y => y.SecretToken == token).FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            var user = AutoMapper.Mapper.Map<UserModel>(result.User);

            return user;
        }
    }
}