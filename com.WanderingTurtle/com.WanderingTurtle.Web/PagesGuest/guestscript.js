
console.log("I hate JS");
$(document).ready(function () {
    
    pageLoad();
    $('#MainContent_gvListings').DataTable({
        stateSave: true
    });


});

function pageLoad() {

    console.log("jquery is ready.2");
    $("#txtGuestTickets").spinner({
        min: 0,
        max: $("#hfGuestMaxTickets").val()
    });

    $("#txtGuestTickets").change(function () {
        if (($.isNumeric($(this).val()))) {
            if ($(this).val() > 0 && $(this).val() < $("#hfGuestMaxTickets").val()) {
                $(this).css("background-color", "white");
                return;
            }
            else {
                $(this).css("background-color", "red");
            }
        }
        else {
            $(this).css("background-color", "red");
        }
    });

    $("#txtGuestPin").change(function () {
        if (pinValue.length != 6) {
            $(this).css("background-color", "red");
        }
        else {
            $(this).css("background-color", "white");
        }

    });

    
}

function showMyMessage() {

    console.log("this works");
    $('#otherMessage').dialog({
        autoOpen: false,
        width: 'auto',
        resizable: false,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).close;
        }
    });

    $("#otherMessage").dialog("open");


}