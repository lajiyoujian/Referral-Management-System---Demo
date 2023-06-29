$(document).ready(function () {
    // Show popup on logout link click
    var isLogoutPopupOpen = false;

    // Show/hide popup on logout link click
    $('#logoutLink').click(function (e) {
        e.preventDefault();
        if (!isLogoutPopupOpen) {
            $('#logoutPopup').show();
            isLogoutPopupOpen = true;
        } else {
            $('#logoutPopup').hide();
            isLogoutPopupOpen = false;
        }
    });

    //// Hide popup on cancel button click
    //$('#logoutPopup').on('click', '#logoutCancel', function () {
    //    $('#logoutPopup').hide();
    //});

    //// Handle logout confirmation
    //$('#logoutPopup').on('click', '#logoutConfirm', function () {
    //    // Perform logout action here
    //    // ...
    //    // After logout, redirect the user to the desired page
    //    window.location.href = '/index'; // Replace '/index' with the actual page URL
    //});


});
