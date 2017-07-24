using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
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
        private readonly IAddNewPub _addNewPub;
        private readonly IConfirmUserEmailByToken _confirmUserEmailByToken;
        private readonly IAddNewUser _addNewUser;
        private readonly IAddNewMeal _addNewMeal;

        public DbContextSeeder(IAddNewPub addNewPub,
            IConfirmUserEmailByToken confirmUserEmailByToken,
            IAddNewUser addNewUser,
            IAddNewMeal addNewMeal)
        {
            _addNewPub = addNewPub;
            _confirmUserEmailByToken = confirmUserEmailByToken;
            _addNewUser = addNewUser;
            _addNewMeal = addNewMeal;
        }

        public void Seed()
        {
            SeedUser(_addNewUser, _confirmUserEmailByToken);
            SeedPub(_addNewPub, _addNewMeal);
        }

        private readonly string[] users = { "Janusz", "Staszek", "Mietek" };
        private readonly string[] pubs = { "Kantyna", "Schadzka", "Ha-Noi", "Pyszna Bułka", "Donner Kebab" };
        private readonly string[] meals = { "Kaczka", "Kurczak", "Kot", "Pies", "Szablozębna ośmiornica" };

        public void SeedPub(IAddNewPub _addNewPub, IAddNewMeal _addNewMeal)
        {
            var MealExpense = 10.5m;

            foreach (var pub in pubs)
            {
                var model = new PubModel
                {
                    Name = $"{pub}",
                    Adress = $"{pub}@ w Legnicy"
                };

               var PubModell =  _addNewPub.Invoke(model);

                foreach (var meal in meals)
                {
                    var mealModel = new MealModel
                    {
                        Name = $"{meal}",
                        Type = "Jedzenie",
                        Expense = MealExpense,
                    };
                    MealExpense += 1.3m;
                    _addNewMeal.Invoke(mealModel, PubModell.Id);
                }
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