/**
 * Declaration,
 */
var userId = $("#SignedDiv").data("id");
var userName = $("#SignedDiv").data("name");
var now = new Date();
var InJoinedOrderID;
var InJoinedOrderPubID;
var MealsToDeleteID;
var MealList;
var listoRDER;
var dateEndTime;
var execute = false;

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
        if (execute !== true) {
            myHub.server.getMealsinCompleteOrder(InJoinedOrderID);
            executed = true;
        }
    }
    if (title.html() === "Please add your meals,Thanks man!") {
        $("#MealsInOrderTable").ready(function () {
            if ($("#MealsInOrderTable").data("orderid") !== null) {
                InJoinedOrderID = $("#MealsInOrderTable").data("orderid");
            } console.log("dupa z tym" + InJoinedOrderID);

            var mealsInOrder = myHub.server.getUserOrders(InJoinedOrderID);
            $.when(mealsInOrder).then(function () {
                myHub.server.getMealfromPub(InJoinedOrderID);
                // Set the date we're counting down to
                dateEndTime = $("#MealsInOrderTable").data("orderendtime");
                var countDownDate = dateEndTime;

                // Update the count down every 1 second
                var x = setInterval(function () {
                    // Get todays date and time
                    console.log(countDownDate);
                    var now = new Date().getTime();

                    // Find the distance between now an the count down date
                    var distance = countDownDate - now;

                    // Time calculations for days, hours, minutes and seconds
                    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

                    // Output the result in an element with id="demo"
                    if (days === 0) {
                        $("#daysCountDown").css("cssText", "color:#e30613");
                        document.getElementById("daysCountDown").innerHTML = days;
                    } else {
                        $("#daysCountDown").css("cssText", "color:black");
                        document.getElementById("daysCountDown").innerHTML = days;
                    }

                    if (hours === 0) {
                        $("#hoursCountDown").css("cssText", "color:#e30613");
                        document.getElementById("hoursCountDown").innerHTML = hours;
                    } else {
                        $("#hoursCountDown").css("cssText", "color:black");
                        document.getElementById("hoursCountDown").innerHTML = hours;
                    }

                    if (minutes === 0) {
                        $("#minutesCountDown").css("cssText", "color:#e30613");
                        document.getElementById("minutesCountDown").innerHTML = minutes;
                    } else {
                        $("#minutesCountDown").css("cssText", "color:black");
                        document.getElementById("minutesCountDown").innerHTML = minutes;
                    }

                    if (seconds === 0) {
                        $("#secondsCountDown").css("cssText", "color:#e30613");
                        document.getElementById("secondsCountDown").innerHTML = seconds;
                    } else {
                        $("#secondsCountDown").css("cssText", "color:black");
                        document.getElementById("secondsCountDown").innerHTML = seconds;
                    }

                    // If the count down is over, write some text
                    if (distance < 0) {
                        var urlss = "/Order/ActiveOrders";
                        window.location.href = urlss;
                    }
                }, 1000);
            });
        });
    }
    if (title.html() === "Submited Order:") {
        InJoinedOrderID = $("#MealsInSubmitedOrderTable").data("orderid");
        myHub.server.getMealsInSubmitedOrder(InJoinedOrderID);
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
    if (result.length === 0) {
        var jaka = $("#HistoryOrderVievs");
        var added = "<i class='massive black ban icon' style='height: 400px; padding-top:100px;'></i>";
        jaka.append(added);
    } else {
        var historyOrdersTable = $("#HistoryOrderTable");
        $.each(result,
            function(index, value) {
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
                        value.Expense.toFixed(2) +
                        " zł</td>" +
                        "<td>";
                    divToAppend +=
                        '<a id ="GoToSummaryView" class="ui blue button" href="/Order/SummaryOrderInHistory/ ' +
                        value.OrderId +
                        '">Look Order</a>';

                    divToAppend += "</td>" +
                        "</tr>";
                    historyOrdersTable.append(divToAppend);
                }
            });
    }
};
myHub.client.getMealsInSubmitedOrder = function (result) {
    console.log("Load Meal in Complete orders in History");
    var mealsInOrderTable = $("#MealsInSubmitedOrderTable");
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
                value.Expense.toFixed(2) +
                " zł</td>" +
                "</tr>";
            mealsInOrderTable.append(divToAppend);
        });
};

/*
* Load Active Orders,
*/
myHub.client.loadActivePubOrders = function (result) {
    console.log("Load Active Orders");
    listoRDER = result;
    if (result.length === 0) {
        var jaka = $("#ActiveOrderVievs");
        var added = "<i class='massive black ban icon' style='height: 400px; padding-top:100px;'></i>";
        jaka.append(added);
    } else {
        var activeOrdersTable = $("#ActiveOrderTable");
        $.each(result,
            function(index, value) {
                InJoinedOrderID = value.OrderId;
                var divToAppend = '<tr id="ActiveOrderTableRow" data-orderid="' +
                    value.OrderId +
                    '" data-PubOrderId="' +
                    value.PubOrderId +
                    '">' +
                    "<td>" +
                    value.OrderName +
                    "</td>" +
                    "<td>" +
                    value.PubName +
                    "</td>" +
                    "<td>" +
                    value.OwnerName +
                    "</td>" +
                    "<td>" +
                    value.CreatedOn +
                    "</td>" +
                    "<td id='EndTimeInActive' data-value='" +
                    value.EndDateTimeData +
                    "'>" +
                    value.EndDateTime +
                    "</td>" +
                    "<td id='ActionsInActive'>";
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
    }
};
setInterval(doSomething, 5000);

function doSomething() {
    var dateTimeNow = new Date();
    $('#ActiveOrdersTableRow tr').each(function () {
        var dateTime = $(this).find("#EndTimeInActive").data("value");
        var orderId = $(this).data("orderid");
        if (dateTime < dateTimeNow) {
            $(this).find("#ActionsInActive").html('<a id ="GoToSummaryView" class="ui orange button" href="/Order/SummaryOrder/ ' + orderId + '">Summary</a>');
        }
    });
}
/*
 * Modal, Load Meal In FormModal,
 */
myHub.client.pubMealLoadedAction = function (mealList, mealTypeList, freeDeliveryInPub) {
    console.log("Load PubMeal");
    console.log("Darmowa dostawa od", freeDeliveryInPub);

    document.getElementById("ExpenseCountDown").innerHTML = freeDeliveryInPub;
    MealList = mealList;
    $("#MealTypeinOrderSelect").find("option")
        .remove()
        .end();
    $.each(mealTypeList,
        function (index, item) {
            $("#MealTypeinOrderSelect").append($("<option>",
                { text: item.MealTypeName, value: item.MealTypeId, selected: false },
                "</option>"));
        });
};
function SendListToView(selectedType) {
    console.log("Load Meal when Type is Selected", selectedType);
    $("#newMealinOrderSelect").find("option")
        .remove()
        .end();
    $.each(MealList,
        function (index, item) {
            if (item.TypeMealId === selectedType) {
                $("#newMealinOrderSelect").append($("<option>",
                    { text: (item.MealName + " " + item.Expense + " zł"), value: item.MealId, selected: false },
                    "</option>"));
            }
        });
}
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
            result.Expense.toFixed(2) +
            " zł</td>" +
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
var executed = false;
$("#createOrderForm").submit(function (e) {
    e.preventDefault();
    var data = serializeForm($(this));
    var hubIsReadyNow = $.connection.hub.start();
    $.when(hubIsReadyNow).then(function () {
        if (!executed) {
            myHub.server.addNewPubOrder(data);
            executed = true;
            console.log("Order is added.");
        }
    });
});
myHub.client.addNewPubOrder = function (result) {
    console.log("Somebody add Puborder", result.OwnerId);
    var activeOrdersTable = $("#ActiveOrderTable");
    InJoinedOrderID = result.OrderId;

    var divToAppend = "<tr id='ActiveOrderTable' data-PubOrderId='" +
        result.PubOrderId +
        "'>" +
        "<td>" +
        result.OrderName +
        "</td>" +
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
                value.Expense.toFixed(2) +
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
        var deletedOrder = $('#ActiveOrderTable tr[data-puborderid="' + result + '"]');
        console.log("Klient usuwa zamowienie o id = ", result);
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
    var userExpense = 0;

    if (result.length === 0) {
        var jaka = $("#CompleteOrderViews");
        var added = "<i class='massive black ban icon' style='height: 400px; padding-top:100px;'></i>";
        jaka.append(added);
    }
    else {
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
                    value.Expense.toFixed(2) +
                    " zł</td>" +
                    "</tr>";
                mealsInOrderTable.append(divToAppend);

                if (userId === value.UserId) {
                    userExpense = userExpense += value.Expense;
                }
            });

        var divSecendToAppend = "<tr id ='orderMealid' >" +
            "<td></td>" +
            "<td></td>" +
            "<td></td>" +
            "<td style='color: red;'> User cost: " + userExpense.toFixed(2) + " zł</td>" +
            "</tr>";
        mealsInOrderTable.append(divSecendToAppend);

        if (userId === id) {
            var orderCompleteView = $("#OrderMealList");
            var divToAppend2 = '<a onClick="openSubmittSideBar(\'' + orderId + '\');" class="ui green button"><i class="call icon"></i> Submit Order</a>';
            orderCompleteView.append(divToAppend2);
        }
    }
};
/*
*Submit Complete Order
*/
$("#SubmitCompleteOrderSidebar").first().sidebar('setting', 'transition', 'overlay').sidebar();
function openSubmittSideBar(orderId) {
    var sidebar = $("#SubmitCompleteOrderSidebar");
    var puher = $(".pushable");

    if (sidebar.hasClass("visible")) {
        puher.removeAttr("style");
        sidebar.removeClass("visible");
    } else {
        myHub.server.getPubInfo(orderId);
        sidebar.addClass("visible");
        puher.css("cssText", "margin-left: 200px !important");
    }
    InJoinedOrderID = orderId;
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
    var sidebar = $("#SubmitCompleteOrderSidebar");
    puher.removeAttr("style");
    sidebar.removeClass("visible");
}
function submitOrder() {
    console.log("Wysyłam zamowienie o id:", InJoinedOrderID);
    myHub.server.submitOrder(InJoinedOrderID);
}
myHub.client.submitOrder = function (result, orderid) {
    if (result === true) {
        if (InJoinedOrderID === orderid) {
            $("#SubmitCompleteOrderSidebar").removeClass("visible");
            $(".pushable").removeAttr("style");
            console.log("Zamowienie zostało wysłane");
            var urlss = "/Order/OrdersHistory";
            window.location.href = urlss;
        }
    } else {
        console.log("Coś poszło nie tak z wysłaniem zamowienia");
    }
};
/*
 *Edit pub
 */
$(".ui.accordion").accordion();

/*
 * chart
 */

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