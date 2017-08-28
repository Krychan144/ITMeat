using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using ITMeat.BusinessLogic.Action.Adds.Interfaces;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Action.User.Implementations;
using ITMeat.BusinessLogic.Action.User.Interfaces;
using ITMeat.BusinessLogic.Configuration.Interfaces;
using ITMeat.BusinessLogic.Models;
using ITMeat.DataAccess.Repositories.Interfaces;
using Quartz.Impl.Matchers;

namespace ITMeat.BusinessLogic.Configuration.Implementations
{
    public class DbContextSeeder : IDbContextSeeder
    {
        private readonly IAddNewPub _addNewPub;
        private readonly IConfirmUserEmailByToken _confirmUserEmailByToken;
        private readonly IAddNewUser _addNewUser;
        private readonly IAddNewMeal _addNewMeal;
        private readonly IAddNewMealType _addNewMealType;
        private readonly IAddNewAdds _addNewAdds;

        public DbContextSeeder(IAddNewPub addNewPub,
            IConfirmUserEmailByToken confirmUserEmailByToken,
            IAddNewUser addNewUser,
            IAddNewMeal addNewMeal,
            IAddNewMealType addNewMealType,
            IAddNewAdds addNewAdds)
        {
            _addNewPub = addNewPub;
            _confirmUserEmailByToken = confirmUserEmailByToken;
            _addNewUser = addNewUser;
            _addNewMeal = addNewMeal;
            _addNewMealType = addNewMealType;
            _addNewAdds = addNewAdds;
        }

        public void Seed()
        {
            SeedUser(_addNewUser, _confirmUserEmailByToken);
            SeedPub(_addNewPub, _addNewMeal, _addNewMealType);
        }

        private readonly string[] _users = { "Janusz", "Staszek", "Mietek" };
        private readonly string[] _pubs = { "Kantyna", "Schadzka", "Ha-Noi", "Pyszna Bułka", "Donner Kebab" };
        private readonly string[] _mealsDinner = { "Kaczka", "Kurczak", "Wołowina", "Wieprzowina", "Baranina" };
        private readonly string[] _mealsBreakfast = { "Jajecznica", "Naleśniki", "Omlet", "Kełbaski", "Parówki" };
        private readonly string[] _mealsSupper = { "Jajecznica", "Tosty", "Racuchy", "Kanapki", "Naleśniki" };
        private readonly string[] _types = { "Obiad", "Sniadanie", "Kolacja" };
        private readonly string[] _adds = { "Ketchup", "ser", "jajko", "pojemniki" };

        public void SeedPub(IAddNewPub _addNewPub, IAddNewMeal _addNewMeal, IAddNewMealType _addNewMealType)
        {
            var mealExpense = 10.5m;
            var freeDelivery = 50.0m;
            var addsExpense = 1.0m;
            foreach (var pub in _pubs)
            {
                var model = new PubModel
                {
                    Name = $"{pub}",
                    Adress = $"{pub}@ w Legnicy",
                    Phone = 533532578,
                    FreeDelivery = freeDelivery
                };
                freeDelivery += 20.0m;
                var pubModell = _addNewPub.Invoke(model);

                var dinnerMealTypeModel = new MealTypeModel
                {
                    Name = _types[0]
                };
                var dinnerMealType = _addNewMealType.Invoke(dinnerMealTypeModel);

                var supperMealTypeModel = new MealTypeModel
                {
                    Name = _types[2]
                };
                var supperMealType = _addNewMealType.Invoke(supperMealTypeModel);

                var breakfastMealTypeModel = new MealTypeModel
                {
                    Name = _types[1]
                };
                var breakfastMealType = _addNewMealType.Invoke(breakfastMealTypeModel);

                foreach (var meal in _mealsDinner)
                {
                    var mealsDinnerModel = new MealModel
                    {
                        Name = $"{meal}",
                        Expense = mealExpense,
                    };
                    mealExpense += 6.3m;

                    if (dinnerMealType != Guid.Empty)
                    {
                        _addNewMeal.Invoke(mealsDinnerModel, pubModell.Id, dinnerMealType);
                    }
                }

                foreach (var meal in _mealsSupper)
                {
                    var mealsSupperModel = new MealModel
                    {
                        Name = $"{meal}",
                        Expense = mealExpense,
                    };
                    mealExpense += 6.3m;

                    if (supperMealType != Guid.Empty)
                    {
                        _addNewMeal.Invoke(mealsSupperModel, pubModell.Id, supperMealType);
                    }
                }

                foreach (var meal in _mealsBreakfast)
                {
                    var mealsBreakfastModel = new MealModel
                    {
                        Name = $"{meal}",
                        Expense = mealExpense,
                    };
                    mealExpense += 6.3m;

                    if (breakfastMealType != Guid.Empty)
                    {
                        _addNewMeal.Invoke(mealsBreakfastModel, pubModell.Id, breakfastMealType);
                    }
                }

                foreach (var add in _adds)
                {
                    var addsModel = new AddsModel
                    {
                        Name = $"{add}",
                        Expense = addsExpense,
                    };
                    addsExpense += 0.3m;

                    if (breakfastMealType != Guid.Empty)
                    {
                        _addNewAdds.Invoke(addsModel, pubModell.Id);
                    }
                }
            }
        }

        public void SeedUser(IAddNewUser _addNewUser,
            IConfirmUserEmailByToken _confirmUserEmailByToken)
        {
            foreach (var user in _users)
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