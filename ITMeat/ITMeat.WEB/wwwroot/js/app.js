//declaration
var userId = $("#SignedDiv").data("id");
var userName = $("#SignedDiv").data("name");
var now = new Date();
var InJoinedOrderID;

//View Settings
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
 * Load data from server when server connected
 */
function onLoadView() {
    var title = $(".titlename");
    if (title.html() === "Active Orders") {
        myHub.server.getActivePubOrders();
    }
}

/*
 * Create connection SignalR
 */
$.connection.hub.url = "http://localhost:49537/signalr";
var myHub = $.connection.appHub;
//***************************************************************************************************************************
/*
 * Start the connection
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

/*
* Apps functions
*/

//Send Model from Form
function serializeForm(form) {
    this.data = $(form).serializeArray();
    var obj = {};
    $.each(data, function (key, value) {
        obj[value.name] = value.value;
    });
    return obj;
}

/*
 * Order History
 */
//ToDO

/*
* Load Active Orders
*/
myHub.client.loadActivePubOrders = function (result) {
    console.log("Load Active Orders");
    var ActiveOrdersTable = $("#ActiveOrderTable");
    $.each(result,
        function (index, value) {
            console.log(value.OrderId);
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
                        '<a id ="JoinToOrderButton" onclick="loadPubOrders(' +
                        value.OrderId +
                        ');"class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Join</a>';
                } else {
                    divToAppend += "<a class='ui button'>Remove</a>" +
                        '<a id ="JoinToOrderButton" onclick="loadPubOrders(' +
                        value.OrderId +
                        ');"class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Continue</a>';
                }
            } else {
                if (value.IsJoined === false) {
                    divToAppend += '<a id ="JoinToOrderButton" onclick="loadPubOrders(' +
                        value.OrderId +
                        ');"class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Join</a>';
                } else {
                    divToAppend += '<a id ="JoinToOrderButton" onclick="loadPubOrders(' +
                        value.OrderId +
                        ');"class="ui positive button" href="/Order/JoinToPubOrders/ ' +
                        value.OrderId +
                        '">Continue</a>';
                }
            }
            divToAppend += "</td>" +
                "</tr>";
            ActiveOrdersTable.append(divToAppend);
        });
};

/*
 * Get Added Meals To Joined PubOrder
 */
myHub.client.getAddedMealsToJoinedPubOrder = function (result) {
    console.log("Load User Meals in Orders");
    var MealsInOrderTable = $("#MealsInOrderTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr>" +
                "<td>" +
                value.MealName +
                "</td>" +
                "<td>" +
                value.MealName +
                "</td>" +
                "<td>" +
                1 +
                "</td>" +
                "<td>" +
                value.Expense +
                "</td>" +
                "<td>";

            if (value.UserId === userId) {
                divToAppend += "<a class='ui button'>Remove</a>";
            }
            divToAppend += "</td>" +
                "</tr>" +
                "";
            MealsInOrderTable.append(divToAppend);
        });
};

$("#MealsInOrderViews").ready(function () {
    $.when($.connection.hub.start()).then(function () {
        InJoinedOrderID = $("#MealsInOrderTable").data("OrderId");
        console.log("dupa z tym" + InJoinedOrderID);
        myHub.server.getAddedMealsToJoinedPubOrder(InJoinedOrderID);
    });
});
/*
 * Modal, LoadMealInModal,
 */
myHub.client.pubMealLoadedAction = function (result) {
    console.log("Load PubMeal");
    $.each(result,
        function (index, item) {
            $("#newMealinOrderSelect").append($("<option>",
                { text: item.MealName, value: item.MealId, selected: false },
                "</option>"));
        });
};

function AddNewMeal(orderId) {
    console.log("Add new Meal");
    AddMealModal = $(".ui.basic.add-order.modal");
    /**
     * Show modal/settings
     */
    $(AddMealModal).modal('setting', 'closable', false)
        .modal('show');
    /*
     * Load PubMealsfromDataBase
     */
    $.when($.connection.hub.start()).then(function () {
        myHub.server.getMealfromPub(orderId);
    });
}