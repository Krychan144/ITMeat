using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class ChangePassword : IChangePassword
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public ChangePassword(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public bool Invoke(string email, string passwordOld, string newPassword, Guid userId)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwordOld) || string.IsNullOrEmpty(newPassword) ||
                userId == Guid.Empty)
            {
                return false;
            }

            var dbUser = _userRepository.GetById(userId);
            if (dbUser == null)
            {
                return false;
            }

            if (dbUser.Email != email)
            {
                return false;
            }

            var salt = dbUser.PasswordSalt;
            var hashedPassword = _hasher.CreatePasswordHash(passwordOld, salt);

            if (hashedPassword != dbUser.PasswordHash)
            {
                return false;
            }

            var changeUser = AutoMapper.Mapper.Map<DataAccess.Models.User>(dbUser);
            changeUser.PasswordSalt = _hasher.GenerateRandomSalt();
            changeUser.PasswordHash = _hasher.CreatePasswordHash(newPassword, changeUser.PasswordSalt);

            _userRepository.Edit(changeUser);
            _userRepository.Save();

            return true;
        }
    }
}