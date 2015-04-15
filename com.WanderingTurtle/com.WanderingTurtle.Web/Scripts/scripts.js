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
        modal: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        closeOnEscape: true,
        closeText: "X",
        dialogClass: "events",
        autoOpen: true
    }

       );

    var inside = document.getElementById('MainContent_eventdetails');
    if ((inside.innerText.replace(" ", "") =="")) {
        $('#MainContent_eventdetails').dialog('close');
    }
    

});

function showDetails() {

    $('#MainContent_eventdetails').dialog('open');


    }