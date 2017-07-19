﻿$(".ui.sidebar.left").sidebar("setting", {
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

/*
 * Start the connection
 */

$.connection.hub.start()
    .done(function () {
        console.log("Connected to Hub.");
        myHub.server.getActiveOrders();
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
        url: "/User/NewOrder/",
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
* Load Active Orders
*/

var SubmitOrdersViews = function () {
    $.ajax({
        async: true,
        type: "Get",
        url: "/User/SubmitOrder/",
        success: function (result) {
            if (result !== null) {
                console.log("Poprawnie ");
            } else {
                console.log("Nie popeawnie ");
            }
        },
        error: function () { console.log("Cos nie tak ."); }
    });
};

myHub.client.loadActiveOrders = function (result) {
    console.log("Siema siema siema sieman siema iesiema siem asie msis ssiema sie a");
    var ActiveOrdersTable = $("#ActiveOrderTable");
    $.each(result,
        function (index, value) {
            var divToAppend = "<tr>" +
                "<td>" + value.PubName + "</td>" +
                "<td>" + value.CreatedOn + "</td>" +
                "<td>" + value.EndDateTime + "</td>" +
                "<td>";

            if (value.OwnerId === userId) {
                divToAppend += "<div class='ui buttons'>" +
                    "<button class='ui button'>Remove</button>" +
                    "<div class='or'></div>" +
                    "<button class='ui positive button'  id='JoinButton'>Join </button>" +
                    "</div >";
            } else {
                divToAppend += "<button class='ui positive button' id='JoinButton'>Join</button>";
            }
            divToAppend += "</td>" +
                "</tr>";
            ActiveOrdersTable.append(divToAppend);
        });
}

$("#JoinButton").click(function () {
    console.log("wchodzę");
    SubmitOrdersViews();
});