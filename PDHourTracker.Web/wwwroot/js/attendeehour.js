/*
 * Functions used with adding Attendee Hours to Workshops.
 * 
 * /SignOutSheet/Index
 * 
 * */

// Setup listeners
$(function () {
    // Load initial list of attendees
    //initialAttendeeAutocomplete();

    // Setup Autocomplete for attendee name
    attendeeNameAutocomplete();

    // Setup Autocomplete for agency name
    agencyNameAutocomplete();

    // Submit handler for attendee hour form
    $('#attendeeHourForm').submit(function (event) {
        addAttendeeHours(event);
    });

    // Submit handler for attendee form
    $('#attendeeForm').submit(function (event) {
        addAttendee(event);
    });

    // Submit handler for agency form
    $('#agencyForm').submit(function (event) {
        addAgency(event);
    });

    // Add attendee link click
    $('#addAttendeeLink').click(function (e) {
        e.preventDefault();
        hideAttendeeHourShowAttendee();
    });

    // Show agency form when add agench link is clicked
    $('#addAgencyLink').click(function (e) {
        e.preventDefault();
        hideAttendeeShowAgency();
    });

    // Hide attendee form on Cancel click and
    // put focus back to attendee hour form
    $('#attendeeCancel').click(function (e) {
        e.preventDefault();
        hideAttendeeShowAttendeeHour();
    });

    // Hide agency form on Cancel click and
    // put focus back to attendee form
    $('#agencyCancel').click(function (e) {
        e.preventDefault();
        hideAgencyShowAttendee();
    });

    // Disable attendee hours submit button at first
    disableAttendeeHourSubmit(true);
    disableAttendeeSubmit(true);
    disableAgencySubmit(true);

    // Check attendee hour form when attendee id is updated or PD hours
    $('#attendeeName').change(checkAttendeeHourForm);
    $('#PDHours').change(checkAttendeeHourForm).keyup(checkAttendeeHourForm);

    // Check attendee form when names are updated
    $('#firstName').change(checkAttendeeForm);
    $('#lastName').change(checkAttendeeForm);
    $('#attendeeAgency').change(checkAttendeeForm);

    // Check agency form when agency name is updated
    $('#agency').change(checkAgencyForm).keyup(checkAgencyForm);

    // Disable autocomplete
    //disableAutocomplete();

    // Listener on delete class to remove an attendee hour
    $('.delete').click(function (e) {
        e.preventDefault();
        deleteAttendeeHour($(this));
    });
});

// Disable browser autofill/autocomplete
// This is necessary because Chrome ignores autocomplete="off"
function disableAutocomplete() {
    $('#attendeeName').attr('autocomplete', 'something-new');
}

// Hides attendee hour form and table and shows attendee form
function hideAttendeeHourShowAttendee() {
    // Hide attendee hour form and table
    $('#attendeeHourFormContainer').hide();
    clearAttendeeForm();
    // Show attendee form and focus first name
    $('#attendeeFormContainer').show();
    $('#firstName').focus();
    $([document.documentElement, document.body]).animate({
        scrollTop: $('#firstName').offset().top
    }, 1000);
}

// Hides attendee form and shows attendee hour form
// clears attendee form and any related alerts
function hideAttendeeShowAttendeeHour() {
    clearAttendeeForm();
    clearAttendeeAlert();
    $('#attendeeFormContainer').hide();
    // Show attendee hour form and table
    $('#attendeeHourFormContainer').show();
    // Focus attendee name on form
    $('#attendeeName').focus();
    // Scroll to attendee name
    $([document.documentElement, document.body]).animate({
        scrollTop: $('#attendeeName').offset().top
    }, 1000);
}

// Hides agency form and shows attendee form
// Clears agency form and any related alerts
function hideAgencyShowAttendee() {
    clearAgencyForm();
    clearAgencyAlert();
    $('#agencyFormContainer').hide();
    // Show attendee form
    $('#attendeeFormContainer').show();
    // Focus first name
    $('#firstName').focus();
    // Scroll to first name
    $([document.documentElement, document.body]).animate({
        scrollTop: $('#firstName').offset().top
    }, 1000);
}

// Hides attendee form and shows agency form
function hideAttendeeShowAgency() {
    clearAgencyForm();
    // Hide attendee form
    $('#attendeeFormContainer').hide();
    $('#agencyFormContainer').show();
    $('#agency').focus();
    $([document.documentElement, document.body]).animate({
        scrollTop: $('#agency').offset().top
    }, 1000);
}

// Disable Attendee Hour Submit button
// Accepts a boolean that determines if button will be disabled
function disableAttendeeHourSubmit(disable) {
    $('#attendeeHourSubmit').attr('disabled', disable);
}

// Disable Attendee Submit button
// Accepts a boolean that determines if button will be disabled
function disableAttendeeSubmit(disable) {
    $('#attendeeSubmit').attr('disabled', disable);
}

// Disable Agency Submit button
// Accepts a boolean that determines if button will be disabled
function disableAgencySubmit(disable) {
    $('#agencySubmit').attr('disabled', disable);
}

// Checks all inputs of attendee hour form
// Enables submit button when all inputs are valid
function checkAttendeeHourForm() {
    var canSubmit = true;

    // Check for Attendee selected
    var aId = $('#attendeeId').val();
    if (aId === '')
        canSubmit = false;

    // Check for PD Hours
    var pdHours = $('#PDHours').val();
    if (pdHours === '')
        canSubmit = false;
    else {
        pdHours = parseFloat(pdHours);
        if (pdHours <= 0.00)
            canSubmit = false;
    }

    // Enable/disable submit button
    disableAttendeeHourSubmit(!canSubmit);
}

// Checks all inputs of attendee form
// Enables submit button when all inputs are valid
function checkAttendeeForm() {
    var canSubmit = true;

    // Check first name
    if ($('#firstName').val() === '')
        canSubmit = false;

    // Check last name
    if ($('#lastName').val() === '')
        canSubmit = false;

    // Check agency id
    if ($('#attendeeAgencyId').val() === '')
        canSubmit = false;

    // Enable/disable submit button
    disableAttendeeSubmit(!canSubmit);
}

// Checks all inputs of agency form
// Enables submit button when all inputs are valid
function checkAgencyForm() {
    var cansubmit = true;

    // Check agency name
    if ($('#agency').val() === '')
        cansubmit = false;

    // Enable/disable submit button
    disableAgencySubmit(!cansubmit);
}

// Add attendee PD hours to workshop
function addAttendeeHours(event) {
    event.preventDefault();

    // Disable submit button
    disableAttendeeHourSubmit(true);

    // Clear alert
    $('#attendeeHourAlert').empty();

    // Save form data to db
    var dataModel = {
        AttendeeId: $('#attendeeId').val(),
        WorkshopId: $('#workshopId').val(),
        PDHours: $('#PDHours').val(),
        Comments: $('#comments').val()
    };

    $.ajax({
        url: '/api/attendeehours/add',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(dataModel)
    })
        .done(function (data) {
            // Add attendee to list
            var attendeeHour = {
                id: data.id,
                fullName: $('#attendeeName').val(),
                pdHours: parseFloat(dataModel.PDHours).toFixed(2)
            };
            addAttendeeHourToList(attendeeHour);
            //setupRemoveListener();
            clearAttendeeHourForm();
        })
        .fail(function (jqXHR, textStatus) {
            // Re-enable button
            disableAttendeeHourSubmit(false);
            showAttendeeHourAlert(jqXHR.responseText, 'alert-warning', false);
        });
}

// Add row with attendee and hours to attendee hours table
function addAttendeeHourToList(attendeeHour) {
    var tr = $('<tr/>', { 'data-id': attendeeHour.id, 'class': 'bg-warning' });  // highlight newly added row
    tr.append($('<td/>', { text: attendeeHour.fullName }));
    tr.append($('<td/>', { text: attendeeHour.certId }));
    tr.append($('<td/>', { text: attendeeHour.pdHours }));
    var removeLink = $('<a/>', {
        href: '#',
        'class': 'delete',
        'data-id': attendeeHour.id,
        text: 'Remove'
    });
    var removeTd = $('<td/>').append(removeLink);
    tr.append(removeTd);
    tr.appendTo($('#attendeeContainer > table > tbody'));

    // Setup click listener
    removeLink.click(function (e) {
        e.preventDefault();
        deleteAttendeeHour($(this));
    });

    // remove highlight after 2 seconds
    setTimeout(function () {
        tr.removeClass('bg-warning'); // ffc107
    }, 2000);
}

// Setup attendee hour remove listener
function setupRemoveListener() {
    $('.delete').click(deleteAttendeeHour($(this)));
}

// Add new attendee to DB
function addAttendee(event) {
    event.preventDefault();

    // Disable submit button
    disableAttendeeSubmit(true);

    // Clear alert
    clearAttendeeAlert();

    var dataModel = {
        AgencyId: $('#attendeeAgencyId').val(),
        FirstName: $('#firstName').val(),
        LastName: $('#lastName').val(),
        MiddleName: $('#middleName').val(),
        JobTitle: $('#jobTitle').val(),
        CertId: $('#certId').val()
    };

    $.ajax({
        url: '/api/attendees/add',
        type: 'POST',
        data: JSON.stringify(dataModel),
        contentType: 'application/json'
    })
        .done(function () {
            hideAttendeeShowAttendeeHour();
            // Show message stating attendee was added above attendee hour table
            showAttendeeHourAlert('Attendee added!', 'alert-success', true);
        })
        .fail(function (jqXHR) {
            showAttendeeAlert(jqXHR.responseText, 'alert-warning', false)
        });
}

// Add new agency to DB
function addAgency(event) {
    event.preventDefault();

    // Disable submit button
    disableAgencySubmit(true);

    // Clear alert
    clearAgencyAlert();

    var dataModel = {
        AgencyName: $('#agency').val()
    };

    $.ajax({
        url: '/api/agencies/add',
        type: 'POST',
        data: JSON.stringify(dataModel),
        contentType: 'application/json'
    })
        .done(function () {
            // Clear and hide agency form and put focus back to first name
            showAttendeeAlert('Agency added!', 'alert-success', true);
            hideAgencyShowAttendee();
        })
        .fail(function (jqXHR) {
            showAgencyAlert(jqXHR.responseText, 'alert-warning', false);
        });
}

// Show agency form alert
function showAgencyAlert(alertMsg, alertClass, autoClose) {
    // clear alert first
    clearAgencyAlert();

    var alertDiv = getAlertDiv(alertMsg, alertClass, autoClose);

    alertDiv.appendTo($('#agencyAlert'));
}

// Shows alert with given message
// alertClass is the bootstrap class to use (alert-danger, alert-sucess, etc.)
// autoClose is a boolean to determine whether should close automatically
function showAttendeeHourAlert(alertMsg, alertClass, autoClose) {
    // Empty container first
    $('#attendeeHourAlert').empty();

    var alertDiv = getAlertDiv(alertMsg, alertClass, autoClose);

    alertDiv.appendTo($('#attendeeHourAlert'));
}

function showAttendeeAlert(alertMsg, alertClass, autoClose) {
    // Empty container first
    clearAttendeeAlert();

    var alertDiv = getAlertDiv(alertMsg, alertClass, autoClose);

    alertDiv.appendTo($('#attendeeAlert'));
}

function clearAttendeeAlert() {
    $('#attendeeAlert').empty();
}

function clearAgencyAlert() {
    $('#agencyAlert').empty();
}

function getAlertDiv(alertMsg, alertClass, autoClose) {
    var alertDiv = $('<div/>', {
        'class': 'alert ' + alertClass,
        role: 'alert',
        text: alertMsg
    });
    var closeX = $('<span/>', {
        'aria-hidden': true,
        html: '&times;'
    });
    var closeButton = $('<button/>', {
        type: 'button',
        'class': 'close',
        'data-dismiss': 'alert',
        'aria-label': 'close'
    });
    closeButton.append(closeX);
    closeButton.appendTo(alertDiv);

    // Auto close? If true, close after 3 seconds
    if (autoClose) {
        alertDiv.delay(3000).slideUp(1000, function () {
            $(this).alert('close)');
        });
    }

    return alertDiv;
}

function clearAttendeeHourForm() {
    $('#attendeeId').val('');
    $('#attendeeName').val('');
    $('#PDHours').val('');
    $('#comments').val('');
}

function clearAttendeeForm() {
    $('#attendeeAgencyId').val('');
    $('#attendeeAgency').val('');
    $('#firstName').val('');
    $('#lastName').val('');
    $('#middleName').val('');
    $('#jobTitle').val('');
}

function clearAgencyForm() {
    $('#agency').val('');
}

// Focuses attendee form
function focusAttendeeForm() {
    $('attendeeFormContainer').show();
    $('#firstName').focus();
}

// Hides and clears agency form and focuses attendee form
//function hideAgencyAndFocusAttendee() {
//    clearAgencyForm();
//    $('#agencyFormContainer').hide();
//    focusAttendeeForm();
//}

// search for attendees when at least two characters are entered
function attendeeNameAutocomplete() {
    $('#attendeeName').autocomplete({
        delay: 500,
        source: function (request, response) {
            var searchValue = request.term; // what is entered in text box

            // Call api to search attendees first and last names to contain search term
            $.ajax({
                url: '/api/attendees/autocomplete/' + searchValue,
                type: 'POST',
                contentType: 'application/json'
            })
                .done(function (data) {
                    response($.map(data, function (item) {
                        return item; // return results so widget can create a list
                    }));
                })
                .fail(function () {
                    // show error or attendees not found somewhere
                });
        },
        select: function (event, ui) {  // when one is selected, put id in hidden field
            event.preventDefault();
            $(this).val(ui.item.label);
            $('#attendeeId').val(ui.item.value);
            $('#certId').text(ui.item.certId);
            // Check form to enable submit button if all form is valid
            checkAttendeeHourForm();
        },
        // update aria when menu opens and closes
        open: function (event, ui) {
            $('#attendeeName').attr('aria-expanded', true);
        },
        close: function (event, ui) {
            $('#attendeeName').attr('aria-expanded', false);
        },
        minLength: 2 // at least two characters
    }); // autocomplete
}

// Intial attendee auto complete list for initial page load
function initialAttendeeAutocomplete() {
    $('#attendeeName').autocomplete({
        source: function (request, response) {
            // Call api to get top 20 attendees
            $.ajax({
                url: '/api/attendees/top20',
                type: 'POST',
                contentType: 'application/JSON'
            })
                .done(function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }));
                })
                .fail(function () {
                    // Show error
                });
        },
        select: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
            $('attendeeId').val(ui.item.value);
            // Check form
            checkAttendeeHourForm();
        }
    }); // autocomplete
}

// Search for agencies when at least two characters are entered
function agencyNameAutocomplete() {
    $('#attendeeAgency').autocomplete({
        delay: 500,
        source: function (request, response) {
            var searchValue = request.term; // what is entered in text box

            // Call api to search agencies
            $.ajax({
                url: '/api/agencies/autocomplete/' + searchValue,
                type: 'POST',
                contentType: 'application/json'
            })
                .done(function (data) {
                    response($.map(data, function (item) {
                        return item; // return results to make list
                    }));
                })
                .fail(function () {
                    // show error
                });
        },
        select: function (event, ui) {
            event.preventDefault();
            $(this).val(ui.item.label);
            $('#attendeeAgencyId').val(ui.item.value);
            // Check form to enable submit button if valid
            checkAttendeeForm();
        },
        // update aria when menu opens and closes
        open: function (event, ui) {
            $('#attendeeAgency').attr('aria-expanded', true);
        },
        close: function (event, ui) {
            $('#attendeeAgency').attr('aria-expanded', false);
        },
        minLength: 2 // at least two characters before searching
    });
}



// Delete attendee hour
function deleteAttendeeHour(linkEl) {
    var model = {
        Id: $(linkEl).attr('data-id')
    };

    $.ajax({
        url: '/api/attendeehours/removehour',
        type: 'DELETE',
        data: JSON.stringify(model),
        contentType: 'application/json'
    })
        .done(function (id) {
            // Remove attendee hour from list
            removeAttendeeHour(id);
        });
}

// Remove attendee hour from list/table
function removeAttendeeHour(id) {
    $('tr[data-id=' + id + ']').remove();
}