/* 
 * User Roles 
 */

$(function () {
    // Setup Change listener for user role
    $('.role').change(function () {
        roleClick($(this));
    });
});

// Input radio button for user role clicked action.
// Calls function to do the actual update.
function roleClick(el) {
    //console.log(el.val());
    updateUserRole(el);
}

// Update user role.
// User and role data stored in given element
function updateUserRole(el) {
    //console.log(el.attr('data-id'));
    //console.log(el.attr('data-role-name'));
    //console.log(el.val());

    var dataModel = {
        UserId: el.attr('data-id'),
        RoleName: el.attr('data-role-name'),
        Add: el.val()
    };

    $.ajax({
        url: '/api/users/updaterole',
        type: 'POST',
        data: JSON.stringify(dataModel),
        contentType: 'application/json'
    })
        .fail(function () {
            showRoleAlert('alert-warning', 'User role update failed.');
        });
}

// Show alert message
function showRoleAlert(alertClass, msg) {
    var div = $('<div/>', {
        'class': 'alert ' + alertClass + ' alert-dismissible fade show',
        role: 'alert',
        text: msg
    });
    var button = $('<button/>', {
        type: 'button',
        'class': 'close',
        'data-dismiss': 'alert',
        'aria-label': 'close'
    });
    button.append($('<span/>', {
        'aria-hidden': 'true',
        html: '&times;'
    }));
    div.append(button);
    div.appendTo($('#alertContainer'));
}