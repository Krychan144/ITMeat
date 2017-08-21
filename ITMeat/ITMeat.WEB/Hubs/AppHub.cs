using System;
using ITMeat.BusinessLogic.Action.PubOrder.Interfaces;
using ITMeat.WEB.Helpers;
using ITMeat.WEB.Models;
using ITMeat.WEB.Models.PubOrder;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Action.Meal.Interfaces;
using ITMeat.BusinessLogic.Action.MealType.Interfaces;
using ITMeat.BusinessLogic.Action.Order.Interfaces;
using ITMeat.BusinessLogic.Action.Pub.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrder.Interfaces;
using ITMeat.BusinessLogic.Action.UserOrderMeals.Interfaces;
using ITMeat.BusinessLogic.Helpers.Interfaces;
using ITMeat.WEB.Models.Meal.FormModels;
using ITMeat.WEB.Models.MealType.FormModels;
using ITMeat.WEB.Models.Order;
using ITMeat.WEB.Models.Pub;
using ITMeat.WEB.Models.UserOrderMeals;

namespace ITMeat.WEB.Hubs
{
    public class AppHub : Hub
    {
        private readonly IGetActivePubOrders _getActiveOrders;
        private readonly IGetPubMealByOrderId _getPubMealByOrderId;
        private readonly IGetActiveUserOrders _getUserOrders;
        private readonly IGetUserOrderMeals _getUserOrderMeals;
        private readonly IAddNewMealsToUserOrders _addNewMealsToUserOrders;
        private readonly IDeleteUserOrderMeal _deleteUserOrderMeal;
        private readonly IGetUserOrderByUserAndOrderId _getUserOrderByUserAndOrderId;
        private readonly IGetUserOrderMealById _getUserOrderMealById;
        private readonly IGetMealByUserOrderMealId _getMealByUserOrderMealId;
        private readonly IDeletePubOrderByPubOrderId _deletePubOrderByPubOrderId;
        private readonly IGetPubOrderByPubOrderId _getPubOrderByPubOrderId;
        private readonly ICreateNewPubOrder _createNewPubOrder;
        private readonly IGetUserSubmittedOrders _getUserSubmittedOrders;
        private readonly IGetOrderById _getOrderById;
        private readonly IGetPubInfoByOrderId _getPubInfoByOrderId;
        private readonly ISubmitOrder _submitOrder;
        private readonly IGetMealTypeByPubId _getMealTypeByPubId;
        private readonly IConvertDateTime _convertDateTime;
        private static readonly List<UserConnection> ConnectedClients = new List<UserConnection>();
        private const string TimeStampRepresentation = "dd-MM-yyyy HH:mm";

        public AppHub(IGetActivePubOrders getActiveOrders,
            IGetPubMealByOrderId pubMealByPubOrderId,
            IGetActiveUserOrders getUserOrders,
            IGetUserOrderMeals getUserOrderMeals,
            IDeleteUserOrderMeal deleteUserOrderMeal,
            IGetUserOrderByUserAndOrderId getUserOrderByUserAndOrderId,
            IAddNewMealsToUserOrders addNewMealsToUserOrders,
            IGetUserOrderMealById getUserOrderMealById,
            IGetMealByUserOrderMealId getMealByUserOrderMealId,
            IDeletePubOrderByPubOrderId deletePubOrderByPubOrderId,
            IGetPubOrderByPubOrderId getPubOrderByPubOrderId,
            ICreateNewPubOrder createNewPubOrder,
            IGetUserSubmittedOrders getUserSubmittedOrders,
            IGetOrderById getOrderById,
            IGetPubInfoByOrderId getPubInfoByOrderId,
            ISubmitOrder submitOrder,
            IGetMealTypeByPubId getMealTypeByPubId,
            IConvertDateTime convertDateTime)
        {
            _getActiveOrders = getActiveOrders;
            _getPubMealByOrderId = pubMealByPubOrderId;
            _getUserOrders = getUserOrders;
            _getUserOrderMeals = getUserOrderMeals;
            _deleteUserOrderMeal = deleteUserOrderMeal;
            _getUserOrderByUserAndOrderId = getUserOrderByUserAndOrderId;
            _addNewMealsToUserOrders = addNewMealsToUserOrders;
            _getUserOrderMealById = getUserOrderMealById;
            _getMealByUserOrderMealId = getMealByUserOrderMealId;
            _deletePubOrderByPubOrderId = deletePubOrderByPubOrderId;
            _getPubOrderByPubOrderId = getPubOrderByPubOrderId;
            _createNewPubOrder = createNewPubOrder;
            _getUserSubmittedOrders = getUserSubmittedOrders;
            _getOrderById = getOrderById;
            _getPubInfoByOrderId = getPubInfoByOrderId;
            _submitOrder = submitOrder;
            _getMealTypeByPubId = getMealTypeByPubId;
            _convertDateTime = convertDateTime;
        }

        public override Task OnConnected()
        {
            ConnectedClients.Add(new UserConnection { ConnectionId = Context.ConnectionId, UserId = Context.Actor() });

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userToDelete = ConnectedClients.FirstOrDefault(x => x.UserId == Context.Actor());
            ConnectedClients.Remove(userToDelete);

            return base.OnDisconnected(stopCalled);
        }

        public void GetPubInfo(Guid orderId)
        {
            var pubInformation = _getPubInfoByOrderId.Invoke(orderId);
            var pubInfo = new PubInfoViewModel
            {
                PubId = pubInformation.Id,
                Address = pubInformation.Adress,
                Name = pubInformation.Name,
                Phone = pubInformation.Phone
            };
            Clients.Caller.GetPubInfo(pubInfo);
        }

        public void GetActivePubOrders()
        {
            var activeOrderList = _getActiveOrders.Invoke();
            var userOrderList = _getUserOrders.Invoke(Context.Actor());

            var list = activeOrderList.Select(item => new ActiveOrderViewModel
            {
                OwnerId = item.Order.Owner.Id,
                OwnerName = item.Order.Owner.Name,
                CreatedOn = item.Order.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                OrderId = item.Order.Id,
                ToSubmitet = item.Order.EndDateTime.ToLocalTime() < DateTime.Now ? true : false,
                EndDateTime = item.Order.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubId = item.Pub.Id,
                PubName = item.Pub.Name,
                PubOrderId = item.Id,
                OrderName = item.Order.Name,
                IsJoined = userOrderList.Any(x => x.Id == item.Order.Id),
                EndDateTimeData = _convertDateTime.MilliTimeStamp(item.Order.EndDateTime.ToLocalTime())
            });

            Clients.Caller.LoadActivePubOrders(list);
        }

        public void GetUserSubmittedOrders()
        {
            var userSubmittedOrderList = _getUserSubmittedOrders.Invoke(Context.Actor());

            var list = userSubmittedOrderList.Select(item => new GetSubmitedOrderViewModel
            {
                OwnerId = item.Order.Owner.Id,
                OwnerName = item.Order.Owner.Name,
                CreatedOn = item.Order.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                OrderId = item.Order.Id,
                EndDateTime = item.Order.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubId = item.Pub.Id,
                PubName = item.Pub.Name,
                PubOrderId = item.Id,
                Expense = item.Order.Expense
            });

            Clients.Caller.GetUserSubmittedOrders(list);
        }

        public void GetMealfromPub(Guid orderId)
        {
            var mealList = _getPubMealByOrderId.Invoke(orderId);
            var mealViewList = mealList.Select(item => new LoadPubOrderMealViewModel
            {
                PubId = item.Pub.Id,
                MealName = item.Name,
                Expense = item.Expense,
                MealId = item.Id,
                TypeMealId = item.MealType.Id
            });

            var mealTypeList = _getMealTypeByPubId.Invoke(mealViewList.First().PubId);
            var mealTypeViewList = mealTypeList.Select(item => new MealTypeInAddMealToOrder
            {
                MealTypeId = item.Id,
                MealTypeName = item.Name
            });

            Clients.Caller.PubMealLoadedAction(mealViewList, mealTypeViewList);
        }

        public void GetUserOrders(Guid orderId)
        {
            var userOrderMeal = _getUserOrderMeals.Invoke(orderId);
            var viewList = userOrderMeal.Select(item => new GetUserOrderMealViewModel
            {
                UserName = item.UserOrder.User.Name,
                UserId = item.UserOrder.User.Id,
                Quantity = item.Quantity,
                Expense = item.Quantity * item.PubMeal.Expense,
                MealName = item.PubMeal.Name,
                Id = item.Id
            });

            Clients.Caller.GetUserOrderMeals(viewList);
        }

        public void GetMealsinCompleteOrder(Guid orderId)
        {
            var userOrderMeal = _getUserOrderMeals.Invoke(orderId);
            var viewList = userOrderMeal.Select(item => new GetMealInCompleteOrderViewModel
            {
                UserName = item.UserOrder.User.Name,
                UserId = item.UserOrder.User.Id,
                Quantity = item.Quantity,
                Expense = item.Quantity * item.PubMeal.Expense,
                MealName = item.PubMeal.Name,
                Id = item.Id
            });

            var orderOwnerId = _getOrderById.Invoke(orderId);
            Clients.Caller.GetMealsinCompleteOrder(viewList, orderOwnerId.Owner.Id, orderId);
        }

        public void GetMealsInSubmitedOrder(Guid orderId)
        {
            var userOrderMeal = _getUserOrderMeals.Invoke(orderId);
            var viewList = userOrderMeal.Select(item => new GetMealsInSubmitedOrderViewModel
            {
                UserName = item.UserOrder.User.Name,
                UserId = item.UserOrder.User.Id,
                Quantity = item.Quantity,
                Expense = item.Quantity * item.PubMeal.Expense,
                MealName = item.PubMeal.Name,
                Id = item.Id
            });

            Clients.Caller.GetMealsInSubmitedOrder(viewList);
        }

        public void DeleteUserOrderMeal(Guid userOrderMealId)
        {
            if (userOrderMealId == Guid.Empty)
            {
                return;
            }

            var userOrderMeal = _getPubMealByOrderId.Invoke(userOrderMealId);

            if (userOrderMeal == null)
            {
                return;
            }

            var deleted = _deleteUserOrderMeal.Invoke(userOrderMealId);

            if (deleted)
            {
                Clients.All.DeletedUserOrderMeal(userOrderMealId);
            }
        }

        public void DeletePubOrder(Guid pubOrderId)
        {
            if (pubOrderId == Guid.Empty)
            {
                return;
            }
            //ToDo
            var pubOrder = _getPubOrderByPubOrderId.Invoke(pubOrderId);

            if (pubOrder == null)
            {
                return;
            }
            //
            var deleted = _deletePubOrderByPubOrderId.Invoke(pubOrderId);

            if (deleted)
            {
                Clients.All.DeletedPubOrder(pubOrderId);
            }
        }

        public void AddNewMealToOrder(AddNewMealToOrderViewModel model, Guid orderId)
        {
            {
                var userOrderModel = _getUserOrderByUserAndOrderId.Invoke(Context.Actor(), orderId);
                if (userOrderModel == null)
                {
                    return;
                }

                var addMealAction = _addNewMealsToUserOrders.Invoke(orderId, model.MealId, model.Quantity, userOrderModel);

                if (addMealAction == Guid.Empty)
                {
                    return;
                }

                var addedMeal = _getUserOrderMealById.Invoke(addMealAction);
                if (addedMeal == null)
                {
                    return;
                }

                var meal = _getMealByUserOrderMealId.Invoke(addMealAction);

                var addedMealView = new GetUserOrderMealViewModel
                {
                    Id = addedMeal.Id,
                    Quantity = addedMeal.Quantity,
                    MealName = meal.Name,
                    Expense = addedMeal.Quantity * meal.Expense,
                    UserId = addedMeal.UserOrder.User.Id,
                    UserName = addedMeal.UserOrder.User.Name,
                };

                Clients.All.AddNewMealToUserOrder(addedMealView, orderId);
            }
        }

        public void AddNewPubOrder(AddNewOrderViewModel model)
        {
            var orderAddAction = _createNewPubOrder.Invoke(model.EndOrders, model.OrderName, Context.Actor(), model.PubId);

            if (orderAddAction == Guid.Empty)
            {
                return;
            }

            var addedPubOrder = _getPubOrderByPubOrderId.Invoke(orderAddAction);

            var pubOrder = new ActiveOrderViewModel
            {
                OwnerId = addedPubOrder.Order.Owner.Id,
                OwnerName = addedPubOrder.Order.Owner.Name,
                CreatedOn = addedPubOrder.Order.CreatedOn.ToLocalTime().ToString(TimeStampRepresentation),
                OrderId = addedPubOrder.Order.Id,
                ToSubmitet = addedPubOrder.Order.EndDateTime.ToLocalTime() < DateTime.Now ? true : false,
                EndDateTime = addedPubOrder.Order.EndDateTime.ToLocalTime().ToString(TimeStampRepresentation),
                PubId = addedPubOrder.Pub.Id,
                PubName = addedPubOrder.Pub.Name,
                PubOrderId = addedPubOrder.Id,
                OrderName = addedPubOrder.Order.Name,
                IsJoined = false
            };

            Clients.All.AddNewPubOrder(pubOrder);
        }

        public void SubmitOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return;
            }
            var submitOrders = _submitOrder.Invoke(orderId);
            if (submitOrders == false)
            {
                return;
            }
            Clients.Caller.SubmitOrder(submitOrders);
        }
    }
}