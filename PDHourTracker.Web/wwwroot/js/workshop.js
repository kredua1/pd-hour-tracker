/*
 * Workshop functions
 * 
 * */

$(function () {
    clearPDHours();

    // Show the dates for the selected provider Code
    // Also calls appendProviderCode to show id and code
    updateProviderCode();

    // Update provider code hidden input for appending to 
    // session id when selected code changes
    $('#ProviderCodeId').change(updateProviderCode);

    // Append provider code to session identifier on change
    $('#SessionIdentifier').keyup(appendProviderCode);
    $('#SessionIdentifier').change(appendProviderCode);
});

// Make input for PD Hours empty of value is 0
function clearPDHours() {
    var pdHours = $('#PDHours');
    if (pdHours.val() === '0')
        pdHours.val('');
}

// Append selected provider code to the end of given session identifier
function appendProviderCode() {
    var providerCode = $('#providerCode').val().trim();
    var sessionIdValue = $('#SessionIdentifier').val().replace(providerCode, '');
    $('#sessionIdentifierDisplay').text(sessionIdValue + '-' + providerCode);
}

// Gets ProviderCode object from api and udpates ProviderCode hidden item
function updateProviderCode() {
    var providerCodeId = $('#ProviderCodeId').val();

    $.ajax({
        url: '/api/providercodes/' + providerCodeId,
        type: 'GET',
        contentType: 'application/json'
    })
        .done(function (providerCode) {
            $('#providerCode').val(providerCode.code);
            // Show session identifier with provider code
            appendProviderCode();
        });
}