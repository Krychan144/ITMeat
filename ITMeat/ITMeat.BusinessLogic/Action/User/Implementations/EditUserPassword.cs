using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Helpers.Inplementations;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Action.User.Implementations
{
    public class EditUserPassword : IEditUserPassword
    {
        private readonly IUserRepository userRepository;
        private readonly IHasher hasher;

        public EditUserPassword(
            IUserRepository userRepository,
            IHasher hasher = null)
        {
            this.userRepository = userRepository;
            this.hasher = hasher ?? new Hasher();
        }

        public bool Invoke(Guid id, string plainPassword)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(plainPassword))
            {
                return false;
            }

            var userToEdit = userRepository.GetById(id);

            switch (userToEdit)
            {
                default:
                    var salt = hasher.GenerateRandomSalt();
                    userToEdit.PasswordHash = hasher.CreatePasswordHash(plainPassword, salt);
                    userToEdit.PasswordSalt = salt;

                    userRepository.Edit(userToEdit);
                    userRepository.Save();

                    return true;

                case null:
                    return false;
            }
        }
    }
}