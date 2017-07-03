using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;
using System.Linq;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using Org.BouncyCastle.Security;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class AddNewUser : IAddNewUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IHasher _hasher;

        public AddNewUser(IUserRepository userRepository,
            IUserTokenRepository userTokenRepository,
            IHasher hasher
        )
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _hasher = hasher;
        }

        public DataAccess.Models.User Invoke(Models.UserModel user)
        {
            if (!user.IsValid() || _userRepository.FindBy(x => x.Email == user.Email).Count() > 0)
            {
                return null;
            }

            var newUser = AutoMapper.Mapper.Map<DataAccess.Models.User>(user);
            newUser.PasswordSalt = _hasher.GenerateRandomSalt();
            newUser.PasswordHash = _hasher.CreatePasswordHash(user.Password, newUser.PasswordSalt);

            _userRepository.Add(newUser);

            var newUserToken = new DataAccess.Models.UserToken
            {
                User = newUser,
                SecretToken = _hasher.GenerateRandomGuid()
            };

            _userTokenRepository.Add(newUserToken);
            _userRepository.Save();

            return newUser;
        }
    }
}