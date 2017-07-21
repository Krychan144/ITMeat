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
* Load Active Orders
*/

var SubmitOrdersViews = function () {
    $.ajax({
        async: true,
        type: "Get",
        url: "/Order/SubmitOrder/",
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
                "<td>" +value.PubName +"</td>" +
                "<td>" + value.OwnerName +"</td>" +
                "<td>" +value.CreatedOn +"</td>" +
                "<td>" +value.EndDateTime +"</td>" +
                "<td>";

            if (value.OwnerId === userId) {
                divToAppend += "<a class='ui button'>Remove</a>" +
                    "<a class='ui positive button' href='/Order/SubmitOrder/'" +
                    value.PubId +
                    "'>Join </a>";
            } else {
                divToAppend += "<a class='ui positive button'href='/Order/SubmitOrder/'" + value.PubId + "'>Join</a>";
            }
            divToAppend += "</td>" +
                "</tr>";
            ActiveOrdersTable.append(divToAppend);
        });
};

