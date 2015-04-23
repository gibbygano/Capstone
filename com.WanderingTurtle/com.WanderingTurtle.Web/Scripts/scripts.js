console.log("Javascript is working.");
$(document).ready(function () {
    console.log("JQuery loaded");

    $('#tblmain').DataTable({
        stateSave: true


    });

    $('#tblevents').DataTable({
        stateSave: true


    });

    $('#tblitems').DataTable({
        stateSave: true


    });

    $('#tbl1').DataTable({
        searching: false
    });

    $("#errorMess").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        closeOnEscape: true,
        closeText: "X"
    });

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
    });

    $("#listStartDate").datetimepicker(
           {
               controlType: 'select',
               oneLine: true,
               stepMinute: 10,
               timeFormat: 'hh:mm tt'
           });

    $("#listEndDate").datetimepicker(
           {
               controlType: 'select',
               oneLine: true,
               stepMinute: 10,
               timeFormat: 'hh:mm tt'
           });

    $("#listPrice").spinner({
        min: 0,
        max: 999,
        step: 1,
        start: 0.00,
        numberFormat: "C"
    });

    $("#listTickets").spinner({
        min: 0,
        max: 999,
        step: 1,
        start: 0
    });

    var current2 = $('#listCurrent').val();
    console.log(current2);

    $("#listmax").spinner({
        min: current2,
        max: 999,
        step: 1,
        start: 0
    });



    $("#eventStart").datetimepicker(
        {
            controlType: 'select',
            oneLine: true,
            timeFormat: 'hh:mm tt'
        });
    $("#eventEnd").datetimepicker(
        {
            controlType: 'select',
            oneLine: true,
            timeFormat: 'hh:mm tt'
        });

    /* last function because it will break on most pages */
    var inside = document.getElementById('MainContent_eventdetails');
    if ((inside != null)) {
        if ((inside.innerText.replace(" ", "") == "")) {
            $('#MainContent_eventdetails').dialog('close');
        }
    }



});

function showDetails() {

    $('#MainContent_eventdetails').dialog('open');


}