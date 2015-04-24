console.log("Javascript is working.");
$(document).ready(function () {
    console.log("JQuery loaded");

    $("#MainContent_txtPhoneNumber").mask("(999) 999-9999");
    $("#MainContent_txtZip").mask("99999");
    var allZips = $('#zips').DataTable({
    });

    $('#MainContent_txtZip').keyup(function () {
        var search = $(this).val();
        if (search != null) {
            if (search != "") {
                $('#zips').css('display', 'block');
            }
            else {
                $('#zips').css('display', 'none');
            }
        }

        allZips.search(search).draw();

    });


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
        min: 0.00,
        max: 999.00,
        step: .01,
        numberFormat:"C",
        start: 0.00
        
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



});

function showDetails() {

   


}