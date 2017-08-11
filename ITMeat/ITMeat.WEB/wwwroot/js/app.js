/**
 * Declaration,
 */
var userId = $("#SignedDiv").data("id");
var userName = $("#SignedDiv").data("name");
var now = new Date();
var InJoinedOrderID;
var MealsToDeleteID;

//View Settings,
$(".ui.sidebar.left").sidebar("setting", {
    transition: "overlay"
});
$(".your-clock").FlipClock({
    clockFace: 'TwentyFourHourClock'
});
$(".ui.calendar").calendar({
    type: 'datetime',
    minDate: now,
    ampm: false
});
$("#SignedDiv").html("Signed by: " + userName);

/*
 * Load data from server when server connected,
 */
function onLoadView() {
    var title = $(".titlename");
    if (title.html() === "Active Orders") {
        myHub.server.getActivePubOrders();
    }
    if (title.html() === "Orders History") {
        myHub.server.getUserSubmittedOrders();
    }
    if (title.html() === "Order is complete!") {
        InJoinedOrderID = $("#MealsInCompleteTable").data("orderid");
        myHub.server.getMealsinCompleteOrder(InJoinedOrderID);
    }
    if (title.html() === "Please add your meals,Thanks man!") {
        $("#MealsInOrderTable").ready(function () {
            if ($("#MealsInOrderTable").data("orderid") !== null) {
                InJoinedOrderID = $("#MealsInOrderTable").data("orderid");
            } console.log("dupa z tym" + InJoinedOrderID);
            myHub.server.getUserOrders(InJoinedOrderID);
        });
    }
}

/*
 * Create connection SignalR,
 */
//$.connection.hub.url = "http://localhost:49538/signalr";
var myHub = $.connection.appHub;

/*
* Apps functions,
*/

//Send Model from Form,
function serializeForm(form) {
    this.data = $(form).serializeArray();
    var obj = {};
    $.each(data, function (key, value) {
        obj[value.name] = value.value;
    });
    return obj;
}

/*
 * Order History,
 */
myHub.client.getUserSubmittedOrders = function (result) {
    console.log("Load Orders History");
    var historyOrdersTable = $("#HistoryOrderTable");
    $.each(result,
        function (index, value) {
            InJoinedOrderID = value.OrderId;
            if (value.OwnerId === userId) {
                var divToAppend = "<tr id='HistoryOrderTable' data-PubOrderId='" +
                    value.PubOrderId +
                    "'>" +
                    "<td>" +
                    value.PubName +
                    "</td>" +
                    "<td>" +
                    value.CreatedOn +
                    "</td>" +
                    "<td>" +
                    value.EndDateTime +
                    "</td>" +
                    "<td>" +
                    value.Expense +
                    " zł</td>" +
                    "<td>";
                divToAppend +=
                    '<a id ="GoToSummaryView" class="ui blue button" href="/Order/SummaryOrder/ ' +
                    value.OrderId +
                    '">Look Order</a>';

                divToAppend += "</td>" +
                    "</tr>";
                historyOrdersTable.append(divToAppend);
            }
        });
};

/*
* Load Active Orders,
*/
myHub.client.loadActivePubOrders = function (result) {
    console.log("Load Active Orders");
    var activeOrdersTable = $("#ActiveOrderTable");
    $.each(result,
        function (index, value) {
            InJoinedOrderID = value.OrderId;
            var divToAppend = "<tr id='ActiveOrderTable' data-PubOrderId='" +
                value.PubOrderId +
                "'>" +
                "<td>" +
                value.PubName +
                "</td>" +
                "<td>" +
                value.OwnerName +
                "</td>" +
                "<td>" +
                value.CreatedOn +
                "</td>" +
                "<td>" +
                value.EndDateTime +
                "</td>" +
                "<td>";
            if (value.ToSubmitet === false) {
                if (value.OwnerId === userId) {
                    if (value.IsJoined === false) {
                        divToAppend += '<a class="ui button" onClick="deletePubOrder(\'' +
                            value.PubOrderId +
                            '\')" >Remove</a>' +
                            '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                            value.OrderId +
                            '">Join</a>';
                    } else {
                        divToAppend += '<a class="ui button" onClick="deletePubOrder(\'' +
                            value.PubOrderId +
                            '\')" >Remove</a>' +
                            '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                            value.OrderId +
                            '">Continue</a>';
                    }
                } else {
                    if (value.IsJoined === false) {
                        divToAppend +=
                            '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                            value.OrderId +
                            '">Join</a>';
                    } else {
                        divToAppend +=
                            '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                            value.OrderId +
                            '">Continue</a>';
                    }
                }
            } else {
                divToAppend +=
                    '<a id ="GoToSummaryView" class="ui orange button" href="/Order/SummaryOrder/ ' +
                    value.OrderId +
                    '">Summary</a>';
            }

            divToAppend += "</td>" +
                "</tr>";
            activeOrdersTable.append(divToAppend);
        });
};

/*
 * Modal, Load Meal In FormModal,
 */
myHub.client.pubMealLoadedAction = function (result) {
    console.log("Load PubMeal");
    $("#newMealinOrderSelect").find("option")
        .remove()
        .end();
    $.each(result,
        function (index, item) {
            $("#newMealinOrderSelect").append($("<option>",
                { text: item.MealName, value: item.MealId, selected: false },
                "</option>"));
        });
};

/*
 * Add new Meal to Order
 */
function AddNewMeal(orderId) {
    console.log("Add new Meal");
    var addMealModal = $(".ui.basic.add-order.modal");
    /**
     * Show modal/settings
     */
    $(addMealModal).modal("setting", "closable", false)
        .modal("show");
    /*
     * Load PubMealsfromDataBase
     */
    $.when($.connection.hub.start()).then(function () {
        myHub.server.getMealfromPub(orderId);
    });
}
$("#addMealToOrderForm").submit(function (e) {
    e.preventDefault();
    var data = serializeForm($(this));
    myHub.server.addNewMealToOrder(data, InJoinedOrderID);
    $(".ui.basic.add-order.modal").modal("hide");
});
myHub.client.addNewMealToUserOrder = function (result, id) {
    console.log("Somebody add order");
    var mealsInOrderTable = $("#MealsInOrderTable");
    if (id === $("#MealsInOrderTable").data("orderid")) {
        var divToAppend = "<tr id ='orderMealid' data-usermealid='" +
            result.Id +
            "'>" +
            "<td>" +
            result.UserName +
            "</td>" +
            "<td>" +
            result.MealName +
            "</td>" +
            "<td>" +
            result.Quantity +
            "</td>" +
            "<td>" +
            result.Expense +
            "</td>" +
            "<td>";
        if (result.UserId === userId) {
            divToAppend += '<a class="ui button" onClick="deleteMealFromOrder(\'' + result.Id + '\')">Remove</a>';
        }
        divToAppend += "</td>" +
            "</tr>";
        mealsInOrderTable.append(divToAppend);
    }
};

/*
 * Add new PubOrder
 */
$("#createOrderForm").submit(function (e) {
    e.preventDefault();
    var data = serializeForm($(this));

    $.when($.connection.hub.start()).then
    {
        myHub.server.addNewPubOrder(data);
    }
});
myHub.client.addNewPubOrder = function (result) {
    console.log("Somebody add Puborder", result.OwnerId);
    var activeOrdersTable = $("#ActiveOrderTable");
    InJoinedOrderID = result.OrderId;

    var divToAppend = "<tr id='ActiveOrderTable' data-PubOrderId='" +
        result.PubOrderId +
        "'>" +
        "<td>" +
        result.PubName +
        "</td>" +
        "<td>" +
        result.OwnerName +
        "</td>" +
        "<td>" +
        result.CreatedOn +
        "</td>" +
        "<td>" +
        result.EndDateTime +
        "</td>" +
        "<td>";
    if (result.ToSubmitet === false) {
        if (result.OwnerId === userId) {
            if (result.IsJoined === false) {
                divToAppend += '<a class="ui button" onClick="deletePubOrder(\'' +
                    result.PubOrderId +
                    '\')" >Remove</a>' +
                    '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                    result.OrderId +
                    '">Join</a>';
            } else {
                divToAppend += '<a class="ui button" onClick="deletePubOrder(\'' +
                    result.PubOrderId +
                    '\')" >Remove</a>' +
                    '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                    result.OrderId +
                    '">Continue</a>';
            }
        } else {
            if (result.IsJoined === false) {
                divToAppend +=
                    '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                    result.OrderId +
                    '">Join</a>';
            } else {
                divToAppend +=
                    '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                    result.OrderId +
                    '">Continue</a>';
            }
        }
    } else {
        divToAppend +=
            '<a id ="GoToSummaryView" class="ui orange button" href="/Order/SummaryView/ ' +
            result.OrderId +
            '">Summary</a>';
    }

    divToAppend += "</td>" +
        "</tr>";
    activeOrdersTable.append(divToAppend);
    if (result.OwnerId === userId) {
        var url = 'ActiveOrders';
        window.location.href = url;
    }
};

/*
* Get OrdersMeals,
*/
myHub.client.getUserOrderMeals = function (result) {
    console.log("Load User Meals in Orders");

    var mealsInOrderTable = $("#MealsInOrderTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr id ='orderMealid' data-usermealid='" +
                value.Id +
                "'>" +
                "<td>" +
                value.UserName +
                "</td>" +
                "<td>" +
                value.MealName +
                "</td>" +
                "<td>" +
                value.Quantity +
                "</td>" +
                "<td>" +
                value.Expense +
                " zł</td>" +
                "<td>";
            if (value.UserId === userId) {
                divToAppend += '<a class="ui button" onClick="deleteMealFromOrder(\'' + value.Id + '\')">Remove</a>';
            }
            divToAppend += "</td>" +
                "</tr>";
            mealsInOrderTable.append(divToAppend);
        });
};

/*
 * Delete Meal form orders,
 */
function deleteMealFromOrder(userOrderMeals) {
    console.log("Zarcie to usuniecia", userOrderMeals);
    MealsToDeleteID = userOrderMeals;
    console.log("Delete Meal form Order");
    var deleteMealModal = $("#deleteMealModal");
    /**
     * Show modal/settings
     */
    $(deleteMealModal).modal({
        closable: false,
        onApprove: function () {
            myHub.server.deleteUserOrderMeal(MealsToDeleteID);
        }
    }).modal("show");
}
myHub.client.deletedUserOrderMeal = function (result) {
    if (result !== null) {
        var deletedUserOrderMeal = $("#orderMealid[data-usermealid='" + result + "']");
        console.log("Klient usuwa potrawę z zamowienia o id = " + deletedUserOrderMeal.data("usermealid"));
        deletedUserOrderMeal.hide("slow",
            function () {
                deletedUserOrderMeal.remove();
            });
    } else {
        window.alert("Błąd na stronie, spróbój ponownie z 3 tygodnei ;)");
    }
};

/*
 * Delete Pub Order
 */
function deletePubOrder(value) {
    console.log("Zamowienie do usunięcia", value);
    var pubOrderToDeleteId = value;
    console.log("Delete Pub Order");
    var deleteMealModal = $("#deletePubOrderModal");
    /**
     * Show modal/settings
     */
    $(deleteMealModal).modal({
        closable: false,
        onApprove: function () {
            myHub.server.deletePubOrder(pubOrderToDeleteId);
        }
    }).modal("show");
}
myHub.client.deletedPubOrder = function (result) {
    if (result !== null) {
        var deletedOrder = $("#ActiveOrderTable[data-puborderid='" + result + "']");
        console.log("Klient usuwa zamowienie o id = " + deletedOrder.data("puborderid"));
        deletedOrder.hide("slow",
            function () {
                deletedOrder.remove();
            });
    } else {
        window.alert("Błąd na stronie, spróbój ponownie z 3 tygodnei ;)");
    }
};

/*
 * Summary View
 */
myHub.client.getMealsinCompleteOrder = function (result, id, orderId) {
    console.log("Load Meal in Complete orders");
    var mealsInOrderTable = $("#MealsInCompleteTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr id ='orderMealid' data-usermealid='" +
                value.Id +
                "'>" +
                "<td>" +
                value.UserName +
                "</td>" +
                "<td>" +
                value.MealName +
                "</td>" +
                "<td>" +
                value.Quantity +
                "</td>" +
                "<td>" +
                value.Expense +
                " zł</td>" +
                "</tr>";
            mealsInOrderTable.append(divToAppend);
        });
    if (userId === id) {
        var OrderCompleteView = $("#OrderMealList");
        var divToAppend2 = '<a onClick="openSideBar(\'' + orderId + '\');" class="ui green button"><i class="call icon"></i> Submit Order</a>';
        OrderCompleteView.append(divToAppend2);
    }
};

/*
*Submit Complete Order
*/
$(".ui.sidebar.vertical.inverted.right").first().sidebar('setting', 'transition', 'overlay').sidebar();
function openSideBar(orderId) {
    var sidebar = $(".ui.sidebar.vertical.inverted.right");
    var puher = $(".pushable");

    if (sidebar.hasClass("visible")) {
        puher.removeAttr("style");
        sidebar.removeClass("visible");
    } else {
        myHub.server.getPubInfo(orderId);
        sidebar.addClass("visible");
        puher.css("cssText", "margin-left: 200px !important");
    }
}
myHub.client.getPubInfo = function (result) {
    console.log("Load Pub Informations");
    var name = $("#pubName");
    var phone = $("#pubPhone");
    var address = $("#pubAddress");

    name.html(" Name: " + result.Name);
    phone.html(" Phone: " + result.Phone);
    address.html(" Adress : " + result.Address);
};
function cancelSubmitOrder() {
    var puher = $(".pushable");
    var sidebar = $(".ui.sidebar.vertical.inverted.right");
    puher.removeAttr("style");
    sidebar.removeClass("visible");
}
function submitOrder() {
    console.log("Zamowienie zostało wysłane");
}
//***************************************************************************************************************************
/*
 * Start the connection,
 */
$.connection.hub.start()
    .done(function () {
        console.log("Connected to Hub.");
        onLoadView();
    })
    .fail(function (a) {
        console.log("Not connected! " + a);
    });

$.connection.hub.error = function (error) {
    console.warn(error);
};
$.connection.hub.stateChanged(function (change) {
    if (change.newState === $.signalR.connectionState.reconnecting) {
        console.log("Re-connecting");
    } else if (change.newState === $.signalR.connectionState.connected) {
        console.log("The server is online");
    }
});
$.connection.hub.reconnected(function () {
    console.log("Reconnected");
});
window.onbeforeunload = function () {
    $.connection.hub.stop();
};