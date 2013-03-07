$(function () {
    // Welcome overlay
    $('#welcome').click(function () {
        $('#overlay').fadeIn('fast', function () {
            $('#boxMenu').fadeIn('fast');
            $('#welcomeBox').fadeIn('fast');
        });
    });
    $('#boxclose').click(function () {
        $('#boxMenu').fadeOut('fast');
        $('#welcomeBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlay').hide();
            $('#welcomeBox').css('top', '200px');
        });
    });

    // About overlay
    $('#about').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').fadeIn('fast');
            $('#aboutBox').fadeIn('fast');
        });
    });
    $('#boxclose2').click(function () {
        $('#boxMenu').fadeOut('fast');
        $('#aboutBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#aboutBox').hide();
            $('#aboutBox').css('top', '200px');
        });
    });

    // Privacy & Terms overlay
    $('#privacy').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').fadeIn('fast');
            $('#privacyBox').fadeIn('fast');
        });
    });
    $('#boxclose3').click(function () {
        $('#boxMenu').fadeOut('fast');
        $('#privacyBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#privacyBox').hide();
            $('#privacyBox').css('top', '200px');
        });
    });

    // Help overlay
    $('#help').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').fadeIn('fast');
            $('#helpBox').fadeIn('fast');
        });
    });
    $('#boxclose4').click(function () {
        $('#boxMenu').fadeOut('fast');
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#helpBox').hide();
            $('#helpBox').css('top', '200px');
        });
    });

    // Public channel Help Box
    $('#publicChannel').click(function () {
        $('#helpBox').fadeOut('fast', function () {
            $('#publicChannelBox').fadeIn('fast');
            $('#helpBox').hide();
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous').click(function () {
        $('#publicChannelBox').fadeOut('fast', function () {
            $('#publicChannelBox').hide();
            $('#publicChannelBox').css('top', '200px');
            $('#helpBox').fadeIn('fast');
        });
    });
    $('#boxclose5').click(function () {
        $('#boxMenu').fadeOut('fast');
        $('#publicChannelBox').fadeOut('fast', function () {
            $('#publicChannelBox').hide();
            $('#publicChannelBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // Private channel Help Box
    $('#privateChannel').click(function () {
        $('#helpBox').fadeOut('fast', function () {
            $('#privateChannelBox').fadeIn('fast');
            $('#helpBox').hide();
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous2').click(function () {
        $('#privateChannelBox').fadeOut('fast', function () {
            $('#privateChannelBox').hide();
            $('#privateChannelBox').css('top', '200px');
            $('#helpBox').fadeIn('fast');
        });
    });
    $('#boxclose6').click(function () {
        $('#privateChannelBox').fadeOut('fast', function () {
            $('#privateChannelBox').hide(); ;
            $('#privateChannelBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // Private channel commands Help Box
    $('#privateChannelCommands').click(function () {
        $('#helpBox').fadeOut('fast', function () {
            $('#privateChannelCommandsBox').fadeIn('fast');
            $('#helpBox').hide();
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous3').click(function () {
        $('#privateChannelCommandsBox').fadeOut('fast', function () {
            $('#privateChannelCommandsBox').hide();
            $('#privateChannelCommandsBox').css('top', '200px');
            $('#helpBox').fadeIn('fast');
        });
    });
    $('#boxclose7').click(function () {
        $('#privateChannelCommandsBox').fadeOut('fast', function () {
            $('#privateChannelCommandsBox').hide();
            $('#privateChannelCommandsBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // About option
    $('#aboutOption').click(function () {
        $('#publicChannelBox').fadeOut('fast');
        $('#privateChannelBox').fadeOut('fast');
        $('#privateChannelCommandsBox').fadeOut('fast');
        $('#helpBox').fadeOut('fast');
        $('#privacyBox').fadeOut('fast');
        $('#aboutBox').fadeIn('fast');
    });

    // Privacy option
    $('#privacyOption').click(function () {
        $('#publicChannelBox').fadeOut('fast');
        $('#privateChannelBox').fadeOut('fast');
        $('#privateChannelCommandsBox').fadeOut('fast');
        $('#aboutBox').fadeOut('fast');
        $('#helpBox').fadeOut('fast');
        $('#privacyBox').fadeIn('fast');
    });

    // Help option
    $('#helpOption').click(function () {
        $('#publicChannelBox').fadeOut('fast');
        $('#privateChannelBox').fadeOut('fast');
        $('#privateChannelCommandsBox').fadeOut('fast');
        $('#aboutBox').fadeOut('fast');
        $('#privacyBox').fadeOut('fast');
        $('#helpBox').fadeIn('fast');
    });
});