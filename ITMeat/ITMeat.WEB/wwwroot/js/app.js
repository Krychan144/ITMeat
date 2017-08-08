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
}

/*
 * Create connection SignalR,
 */
$.connection.hub.url = "http://localhost:49538/signalr";
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
//ToDO

/*
* Load Active Orders,
*/
myHub.client.loadActivePubOrders = function (result) {
    console.log("Load Active Orders");
    var activeOrdersTable = $("#ActiveOrderTable");
    $.each(result,
        function (index, value) {
            InJoinedOrderID = value.OrderId;
            var divToAppend = "<tr>" +
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
            if (value.OwnerId === userId) {
                if (value.IsJoined === false) {
                    divToAppend += "<a class='ui button'>Remove</a>" +
                        '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Join</a>';
                } else {
                    divToAppend += "<a class='ui button'>Remove</a>" +
                        '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Continue</a>';
                }
            } else {
                if (value.IsJoined === false) {
                    divToAppend += '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Join</a>';
                } else {
                    divToAppend += '<a id ="JoinToOrderButton" class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Continue</a>';
                }
            }
            divToAppend += "</td>" +
                "</tr>";
            activeOrdersTable.append(divToAppend);
        });
};
$.when($.connection.hub.start()).then(function () {
    $("#MealsInOrderTable").ready(function () {
        if ($("#MealsInOrderTable").data("orderid") !== null) {
            InJoinedOrderID = $("#MealsInOrderTable").data("orderid");
        } console.log("dupa z tym" + InJoinedOrderID);
        myHub.server.getUserOrders(InJoinedOrderID);
    });
});

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
                "</td>" +
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
    var deleteMealModal = $(".ui.mini.modal");
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
        console.log("Klient usuwa zamowienie o id = " + deletedUserOrderMeal.data("usermealid"));
        deletedUserOrderMeal.hide("slow",
            function () {
                deletedUserOrderMeal.remove();
            });
    } else {
        window.alert("Błąd na stronie, spróbój ponownie z 3 tygodnei ;)");
    }
};

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