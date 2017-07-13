$(".ui.sidebar.left").sidebar("setting", {
    transition: "overlay"
});

$(".your-clock").FlipClock({
    clockFace: "TwentyFourHourClock",
});

$(".ui.calendar").calendar({
    type: "date/time",
});
/*
 * Create connection SignalR
 */

$.connection.hub.url = "http://localhost:49537/signalr";
var myHub = $.connection.AppHub;

/*
 * Start the connection
 */

$.connection.hub.start()
    .done(function () {
        console.log("Connected to Hub.");
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
        loadingStart();
    } else if (change.newState === $.signalR.connectionState.connected) {
        console.log("The server is online");
    }
});

$.connection.hub.reconnected(function () {
    console.log("Reconnected");
    loadingStop();
});

window.onbeforeunload = function () {
    $.connection.hub.stop();
};