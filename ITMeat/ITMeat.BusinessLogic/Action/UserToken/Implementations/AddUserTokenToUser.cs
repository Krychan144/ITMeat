using System;
using System.Collections.Generic;
using System.Text;
using ITMeat.BusinessLogic.Helpers.Inplementations;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.UserToken.Implementations
{
    public class AddUserTokenToUser : IAddUserTokenToUser
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public AddUserTokenToUser(IUserTokenRepository userTokenRepository,
            IUserRepository userRepository,
            IHasher hasher = null)
        {
            _userTokenRepository = userTokenRepository;
            _userRepository = userRepository;
            _hasher = hasher ?? new Hasher();
        }

        public string Invoke(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return string.Empty;
            }

            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                return string.Empty;
            }

            var token = _userTokenRepository.GetById(userId);

            if (token != null)
            {
                _userTokenRepository.Delete(token);
                _userTokenRepository.Save();
            }

            var userToken = new DataAccess.Models.UserToken
            {
                User = user,
                SecretToken = _hasher.GenerateRandomGuid()
            };

            _userTokenRepository.Add(userToken);
            _userTokenRepository.Save();

            return userToken.SecretToken;
        }
    }
}