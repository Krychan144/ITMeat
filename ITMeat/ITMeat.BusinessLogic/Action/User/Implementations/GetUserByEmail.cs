using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class GetUserByEmail : IGetUserByEmail
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmail(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel Invoke(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var dbUser = _userRepository.FindBy(x => x.Email == email).FirstOrDefault();

            if (dbUser == null)
            {
                return null;
            }

            var user = AutoMapper.Mapper.Map<UserModel>(dbUser);

            return user;
        }
    }
}