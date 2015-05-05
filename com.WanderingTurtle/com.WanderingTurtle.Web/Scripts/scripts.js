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

    $('#account').on('mouseenter', function () {
        $('#accountOptions').stop( true, true ).slideToggle({

        });
       
    });
    $('#account').on('mouseleave', function () {
        $('#accountOptions').stop( true, true ).slideToggle({

        });
    });
    

var id;
$('.myID').click(function () {

    id = $(this).attr('name');
    console.log(id);
    showConfirm(id);
})

    


});


function showConfirm(id) {
    $('#deletemessage').dialog({
        autoOpen: false,
        width: 'auto',
        resizable: false,
        modal: true,
        buttons: {
            "Delete":function(){
                __doPostBack("delete", id);
                $(this).dialog("close");
            },
            Cancel: function() {
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).close;
        }
    });
    $("#deletemessage").dialog("open");
    //var confirm_value = document.createElement("INPUT");
    //confirm_value.type = "hidden";
    //confirm_value.name = "confirm_value";
    //document.forms[0].appendChild(confirm_value);
 }

function closeWindow() {
    $("#deletemessage").dialog("close");
    return false;
}

function confirmDelete() {
    $("#deletemessage").dialog("close");
    return true;
}

function showMessage() {
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

