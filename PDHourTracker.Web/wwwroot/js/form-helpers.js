// Trims leading/trailing space from a given elements value
// Element must be jQuery object
function trimInput(el) {
    el.val(el.val().trim());
}

// Accepts a comma separated list of fields to trim input for
function setupInputTrims(fields) {
    var fieldList = fields.split(',');
    for (var i = 0; i < fieldList.length; i++) {
        $('#' + fieldList[i].trim()).on('change', function () {
            trimInput($(this));
        });
    }
}

function emptyDefaultDateInput(fields) {
    var fieldList = fields.split(',');
    for (var i = 0; i < fieldList.length; i++) {
        var field = $('#' + fieldList[i]);
        field.val(field.val().replace('1/1/0001', ''));
        field.val(field.val().replace(' 12:00:00 AM', ''));
    }

    // examples below
    //$('#StartDate').val($('#StartDate').val().replace(' 12:00:00 AM', ''));
    //$('#EndDate').val($('#EndDate').val().replace(' 12:00:00 AM', ''));
    //$('#StartDate').val($('#StartDate').val().replace('1/1/0001', ''));
    //$('#EndDate').val($('#EndDate').val().replace('1/1/0001', ''));
}