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
});