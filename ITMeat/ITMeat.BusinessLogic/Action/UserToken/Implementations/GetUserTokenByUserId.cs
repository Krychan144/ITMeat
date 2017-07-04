using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Action.UserToken.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserToken.Implementations
{
    public class GetUserTokenByUserId : IGetUserTokenByUserId
    {
        private readonly IUserTokenRepository userTokenRepository;

        public GetUserTokenByUserId(IUserTokenRepository userTokenRepository)
        {
            this.userTokenRepository = userTokenRepository;
        }

        public UserTokenModel Invoke(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return null;
            }

            var token = userTokenRepository.FindBy(x => x.User.Id == userId).FirstOrDefault();

            if (token == null)
            {
                return null;
            }

            var userTokenModel = Mapper.Map<UserTokenModel>(token);

            return userTokenModel;
        }
    }
}