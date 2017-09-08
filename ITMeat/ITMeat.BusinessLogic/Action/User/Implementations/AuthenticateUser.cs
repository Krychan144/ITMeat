using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Helpers.Inplementations;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class AuthenticateUser : IAuthenticateUser
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;

        public AuthenticateUser(
            IUserRepository userRepository,
            IHasher hasher = null)
        {
            this.userRepository = userRepository;
            this.hasher = hasher ?? new Hasher();
        }

        public UserModel Invoke(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var dbUser = userRepository.FindBy(x => x.Email == email).FirstOrDefault();
            if (dbUser == null)
            {
                return null;
            }

            var salt = dbUser.PasswordSalt;
            var hashedPassword = hasher.CreatePasswordHash(password, salt);

            if (hashedPassword != dbUser.PasswordHash)
            {
                return null;
            }
            var userModel = new UserModel
            {
                Id = dbUser.Id,
                Name = dbUser.Name,
                Email = dbUser.Email,
            };

            return userModel;
        }
    }
}