$(function () {
    $('#welcome').click(function () {
        $('#overlay').fadeIn('fast', function () {
            $('#welcomeBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#boxclose').click(function () {
        $('#welcomeBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlay').fadeOut('fast');
        });
    });

    // About overlay
    $('#about').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#aboutBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#boxclose2').click(function () {
        $('#aboutBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // Privacy & Terms overlay
    $('#privacy').click(function () {
        $('#overlayLB').fadeIn('fast', function () {
            $('#privacyBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#boxclose3').click(function () {
        $('#privacyBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayLB').fadeOut('fast');
        });
    });

    // Help overlay
    $('#help').click(function () {
        $('#overlayLB').fadeIn('fast', function () {
            $('#helpBox').animate({ top: '160px' }, 500);
        });
    });
    $('#boxclose4').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayLB').fadeOut('fast');
        });
    });

    // Public channel Help Box
    $('#publicChannel').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#publicChannelBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#previous').click(function () {
        $('#publicChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#helpBox').animate({ top: '160px' }, 500);
        });
    });
    $('#boxclose5').click(function () {
        $('#publicChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayLB').fadeOut('fast');
        });
    });

    // Private channel Help Box
    $('#privateChannel').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#previous2').click(function () {
        $('#privateChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#helpBox').animate({ top: '160px' }, 500);
        });
    });
    $('#boxclose6').click(function () {
        $('#privateChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayLB').fadeOut('fast');
        });
    });

    // Private channel commands Help Box
    $('#privateChannelCommands').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelCommandsBox').animate({ 'top': '160px' }, 500);
        });
    });
    $('#previous3').click(function () {
        $('#privateChannelCommandsBox').animate({ 'top': '-200px' }, 500, function () {
            $('#helpBox').animate({ top: '160px' }, 500);
        });
    });
    $('#boxclose7').click(function () {
        $('#privateChannelCommandsBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayLB').fadeOut('fast');
        });
    });

    // Feedback link
    $('#feedback').click(function () {
        window.open("https://docs.google.com/forms/d/1pav-_eoF8V522xKQuj0tjt-dTFSGxPsFZ9ct_ZE9ylg/viewform");
    });
    // Sign-up popup
    $('#signUp').click(function () {
        var myWindow = window.open('Register.aspx', '', 'width=800, height=600', 'location=no');
        myWindow.focus();
    });
});