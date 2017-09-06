using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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
        private readonly IAddNewUser _addNewUser;
        private readonly IAddNewMeal _addNewMeal;
        private readonly IAddNewMealType _addNewMealType;

        public DbContextSeeder(IAddNewPub addNewPub,
            IAddNewUser addNewUser,
            IAddNewMeal addNewMeal,
            IAddNewMealType addNewMealType)
        {
            _addNewPub = addNewPub; ;
            _addNewUser = addNewUser;
            _addNewMeal = addNewMeal;
            _addNewMealType = addNewMealType; ;
        }

        public void Seed()
        {
            SeedUser(_addNewUser);
            SeedPub(_addNewPub, _addNewMeal, _addNewMealType);
        }

        private readonly string[] _users = { "Janusz", "Staszek", "Mietek" };
        private readonly string[] _pubs = { "Kantyna", "Schadzka", "Ha-Noi", "Pyszna Bułka", "Donner Kebab" };
        private readonly string[] _mealsDinner = { "Kaczka", "Kurczak", "Wołowina", "Wieprzowina", "Baranina" };
        private readonly string[] _mealsBreakfast = { "Jajecznica", "Naleśniki", "Omlet", "Kełbaski", "Parówki" };
        private readonly string[] _mealsSupper = { "Jajecznica", "Tosty", "Racuchy", "Kanapki", "Naleśniki" };
        private readonly string[] _types = { "Obiad", "Sniadanie", "Kolacja" };

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
            }
        }

        public void SeedUser(IAddNewUser _addNewUser)
        {
            foreach (var user in _users)
            {
                var model = new UserModel
                {
                    Email = $"{user}@test.pl",
                    Password = "test",
                    Name = user,
                };

                _addNewUser.Invoke(model);
            }
        }
    }
}