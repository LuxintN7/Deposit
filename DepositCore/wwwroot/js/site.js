// Write your JavaScript code.

// Deposit tab navigation
$(document).on("click",
    "#generalInfoTabLink",
    function() {
        $.ajax({
            type: "GET",
            url: "/Deposit/GeneralInfo",
            success: function(response) {
                $("#tabMenu").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });

$(document).on("click",
    "#depositListTabLink",
    function() {
        $.ajax({
            type: "GET",
            url: "/Deposit/DepositList",
            success: function(response) {
                $("#tabMenu").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });

$(document).on("click",
    "#openNewDepositTabLink",
    function() {
        $.ajax({
            type: "GET",
            url: "/Deposit/OpenNewDeposit",
            success: function(response) {
                $("#tabMenu").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });

// Add new card actions
$(document).on("click",
    "#addNewCardButton",
    function () {
    $.ajax({
        type: "GET",
        url: "/Deposit/AddCard",
        success: function (response) {
            $("#info-content").html(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});

//$(document).on("click",
//    "#addNewCardConfirmButton",
//    function () {
//        $.ajax({
//        type: "POST",
//        url: "/Deposit/AddCard",
//        data: cardModelData,
//        dataType: "json",
//        success: function (response) {
//            $("#info-content").html(response);
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert(response.responseText);
//        }
//    });
//});
function AddCard() {
    var serializedCard = $("#addNewCardConfirmButton").data("serializedCard");
    alert(JSON.parse(serializedCard));
    $.ajax({
        type: "POST",
        url: "/Deposit/AddCard",
        data: '{ serializedCard:' + serializedCard + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            $("#info-content").html(response);
        },
        failure: function(response) {
            alert(response.responseText);
        },
        error: function(response) {
            alert(response.responseText);
        }
    });
}


// Deposit list actions

//@using(Ajax.BeginForm("CloseDeposit", new { depositId = deposit.Id, cardId = deposit.Card.Id },
//    new AjaxOptions() {HttpMethod = "GET", UpdateTargetId = "deposit-list-content", InsertionMode = InsertionMode.Replace }))
//{
//    <input type="submit" value="Close deposit" class=" btn btn-primary btn-xs" />
//}
$(document).on("click",
    "input[id ^= 'closeDepositSubmitButton']",
    function() {
        var depositIdData = $("#addNewCardButton").data("depositId");
        var cardIdData = $("#addNewCardButton").data("cardId");
        $.ajax({
            type: "GET",
            url: "/Deposit/CloseDeposit",
            data: { depositId: depositIdData, cardId: cardIdData },
            success: function(response) {
                $("#deposit-list-content").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });

//@using(Ajax.BeginForm("NewDeposit", new AjaxOptions() {HttpMethod = "GET", UpdateTargetId = "new-deposit-content", InsertionMode = InsertionMode.Replace }))
//{
//    @Html.Hidden("termsId", depositTerm.Id)
//        <input type="submit" value="Open new deposit" class=" btn btn-primary btn-xs" />
//}

$(document).on("click",
    "input[id ^= 'openNewDepositButton']",
    function () {
        var termsIdData = $(this).attr("data-termsId"); // why is .data not working here?
        $.ajax({
            type: "GET",
            url: "/Deposit/NewDeposit",
            data: { termsId: termsIdData },
            success: function(response) {
                $("#new-deposit-content").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });

$(document).on("click",
    "#openNewDepositConfirmButton",
    function() {
        $.ajax({
            type: "POST",
            url: "/Deposit/NewDeposit",
            success: function(response) {
                $("#new-deposit-content").html(response);
            },
            failure: function(response) {
                alert(response.responseText);
            },
            error: function(response) {
                alert(response.responseText);
            }
        });
    });