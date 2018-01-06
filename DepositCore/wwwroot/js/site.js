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


