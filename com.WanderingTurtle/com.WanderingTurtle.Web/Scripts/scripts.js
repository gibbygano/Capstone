console.log("Javascript is working.");
$(document).ready(function () {
    console.log("JQuery loaded");
    $("#errorMess").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        closeOnEscape: true,
        closeText: "X"
    }
        
        );

    $("#MainContent_eventdetails").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        closeOnEscape: true,
        closeText: "X",
        dialogClass: "events",
        autoOpen: false
    }

       );

    

});

function showDetails() {

 
    $('#MainContent_eventdetails').dialog("open");
    }