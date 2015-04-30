

var startDateTextBox = $('#eventStart');
var endDateTextBox = $('#eventEnd');

$.timepicker.datetimeRange(
    startDateTextBox,
    endDateTextBox,
    {
        minInterval: (1000 * 30 * 60), // 30min
        dateFormat: 'mm/dd/yy',
        timeFormat: 'h:mm:ss TT',
        controlType: 'select',
        oneLine: true,
        showButtonPanel: false,
        stepMinute: 10,
        start: {}, // start picker options
        end: {} // end picker options					
    }
);