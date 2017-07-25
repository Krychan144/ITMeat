$(".ui.sidebar.left").sidebar("setting", {
    transition: "overlay"
});

$(".your-clock").FlipClock({
    clockFace: 'TwentyFourHourClock'
});

var now = new Date();

$(".ui.calendar").calendar({
    type: 'datetime',
    minDate: now,
    ampm: false
});

/*
 * Create connection SignalR
 */
var userId = $("#SignedDiv").data("id");
var userName = $("#SignedDiv").data("name");
$.connection.hub.url = "http://localhost:49537/signalr";
var myHub = $.connection.appHub;
$("#SignedDiv").html("Signed by: " + userName);

function onLoadView() {
    var title = $(".titlename");
    if (title.html() === "Active Orders") {
        myHub.server.getActiveOrders();
    }
}
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

//Create orders room
function serializeForm(form) {
    this.data = $(form).serializeArray();
    var obj = {};
    $.each(data, function (key, value) {
        obj[value.name] = value.value;
    });
    return obj;
};

var createNewOrder = function (data) {
    $.ajax({
        async: true,
        type: "POST",
        url: "/Order/NewOrder/",
        data: data,
        success: function (result) {
            if (result !== null) {
                console.log("Poprawnie ");
            } else {
                console.log("Nie popeawnie ");
            }
        },
        error: function () { console.log("Could not save ."); }
    });
};
/*
 * Order History
 */
function loadPubOrders() {
    console.log("Load User Meals in Orders");
    var MealsInOrderTable = $("#MealsInOrderTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr>" +
                "<td>" + value.UserName + "</td>" +
                "<td>" + value.MealsName + "</td>" +
                "<td>" + value.Quantity + "</td>" +
                "<td>" + value.Expense + "</td>" +
                "<td>";

            if (value.UserId === userId) {
                divToAppend += "<a class='ui button'>Remove</a>";
            }
            divToAppend += "</td>" +
                "</tr>" +
                "";
            MealsInOrderTable.append(divToAppend);
        });
}
/*
* Load Active Orders
*/
myHub.client.loadActiveOrders = function (result) {
    console.log("Load Active Orders");
    var ActiveOrdersTable = $("#ActiveOrderTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr>" +
                "<td>" + value.PubName + "</td>" +
                "<td>" + value.OwnerName + "</td>" +
                "<td>" + value.CreatedOn + "</td>" +
                "<td>" + value.EndDateTime + "</td>" +
                "<td>";
            if (value.OwnerId === userId) {
                divToAppend += "<a class='ui button'>Remove</a>" +
                    '<a id ="JoinToOrderButton" onclick="loadPubOrders(' + value.Id + ');"class="ui positive button" href="/Order/SubmitOrder/ '+ value.Id +'">Join</a>';
                console.log(value.Id);
            } else {
                divToAppend += '<a id ="JoinToOrderButton" onclick="loadPubOrders(' +
                    value.Id +
                    ');"class="ui positive button" href="/Order/SubmitOrder/ ' +
                    value.Id +
                    '">Join</a>';
            }

            divToAppend += "</td>" +
                "</tr>";
            ActiveOrdersTable.append(divToAppend);
        });
};

/*
 * Modal, Add meal to order
 */
myHub.client.PubMealLoadedAction = function (result) {
}

function AddNewMeal(value) {
    console.log("AddnewMeal");
    thisModal = $(".ui.basic.add-order.modal");
    // var orderId = value;
    //console.log(orderId);
    myHub.server.getMealfromPub(value);
    $(thisModal).modal({
        closable: false,
        onDeny: function () {
            $(thisModal).parent().css("background-color", "");
        },
        onApprove: function () {
            $(thisModal).parent().css("background-color", "");
        }
    }).modal("show");

    $(thisModal).parent().css("background-color", "#fff");
};

/*
*Submitt view
*/