using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Action.User.Implementations;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Configuration.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public class DbContextSeeder : IDbContextSeeder
    {
        private readonly IPubRepository _pubRepository;
        private readonly IAddNewPub _addNewPub;
        private readonly IConfirmUserEmailByToken _confirmUserEmailByToken;
        private readonly IAddNewUser _addNewUser;

        public DbContextSeeder(IPubRepository pubRepository,
            IAddNewPub addNewPub,
            IConfirmUserEmailByToken confirmUserEmailByToken,
            IAddNewUser addNewUser)
        {
            _pubRepository = pubRepository;
            _addNewPub = addNewPub;
            _confirmUserEmailByToken = confirmUserEmailByToken;
            _addNewUser = addNewUser;
        }

        public void Seed()
        {
            SeedUser(_addNewUser, _confirmUserEmailByToken);
            SeedPub(_addNewPub);
        }

        private readonly string[] users = { "Janusz", "Staszek", "Mietek" };
        private readonly string[] pubs = { "Kantyna", "Schadzka", "Ha-Noi", "Pyszna Bułka", "Donner Kebab" };

        public void SeedPub(IAddNewPub _addNewPub)
        {
            foreach (var pub in pubs)
            {
                var model = new PubModel
                {
                    Name = $"{pub}",
                    Adress = $"{pub}@ w Legnicy"
                };

                _addNewPub.Invoke(model);
            }
        }

        public void SeedUser(IAddNewUser _addNewUser,
            IConfirmUserEmailByToken _confirmUserEmailByToken)
        {
            foreach (var user in users)
            {
                var model = new UserModel
                {
                    Email = $"{user}@test.pl",
                    Password = "test",
                    Name = user,
                };

                var dbuser = _addNewUser.Invoke(model);

                if (dbuser != null)
                {
                    var token = dbuser.Tokens.FirstOrDefault().SecretToken;
                    _confirmUserEmailByToken.Invoke(token);
                }
            }
        }
    }
}