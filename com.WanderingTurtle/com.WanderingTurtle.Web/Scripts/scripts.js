console.log("Javascript is working.");
$(document).ready(function () {
    console.log("JQuery loaded");

    $('.bxslider').bxSlider({
        mode: 'vertical',
        slideMargin: 5,
        auto: true,
        slideWidth: 350,
        adaptiveHeight: true,
        ticker: true,
        speed: 30000

    });

    $("#MainContent_txtPhoneNumber").mask("(999) 999-9999");
    $("#MainContent_txtZip").mask("99999", 
        {
            placeholder: ""
        });

    //var allZips = $('#zips').DataTable({
    //});

    //$('#MainContent_txtZip').keyup(function () {
    //    var search = $(this).val();

    //    if (search != null) {
    //        console.log(search);
    //        if (search != "") {
    //            $('#zips').css('display', 'block');
    //        }
    //        else {
    //            $('#zips').css('display', 'none');
    //        }
    //    }

    //    allZips.search(search).draw();

    //});


    $('#tblmain').DataTable({
        stateSave: true
    });

    $('#tblevents').DataTable({
        stateSave: true
    });

    $('#tblitems').DataTable({
        stateSave: true,
        autoWidth: false
    });

    $('#tbl1').DataTable({
        searching: false,
        paging: false
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


    $("#listmax").spinner({
        min: $('#listCurrent').val(),
        max: 999,
        step: 1,
        start: 0
    });

    $('#account').hover(function () {
        $('#accountOptions').slideToggle({

        });


    })


});



function showDetails() {

   


}