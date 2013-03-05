$(function () {
    // Welcome overlay
    $('#welcome').click(function () {
        $('#overlay').fadeIn('fast', function () {
            $('#boxMenu').css('visibility', 'visible');
            $('#welcomeBox').css('visibility', 'visible');
        });
    });
    $('#boxclose').click(function () {
        $('#boxMenu').css('visibility', 'hidden');
        $('#welcomeBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlay').fadeOut('fast');
            $('#welcomeBox').css('visibility', 'hidden');
            $('#welcomeBox').css('top', '200px');
        });
    });

    // About overlay
    $('#about').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').css('visibility', 'visible');
            $('#aboutBox').css('visibility', 'visible');
        });
    });
    $('#boxclose2').click(function () {
        $('#boxMenu').css('visibility', 'hidden');
        $('#aboutBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#aboutBox').css('visibility', 'hidden');
            $('#aboutBox').css('top', '200px');
        });
    });

    // Privacy & Terms overlay
    $('#privacy').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').css('visibility', 'visible');
            $('#privacyBox').css('visibility', 'visible');
        });
    });
    $('#boxclose3').click(function () {
        $('#boxMenu').css('visibility', 'hidden');
        $('#privacyBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#privacyBox').css('visibility', 'hidden');
            $('#privacyBox').css('top', '200px');
        });
    });

    // Help overlay
    $('#help').click(function () {
        $('#overlayOrange').fadeIn('fast', function () {
            $('#boxMenu').css('visibility', 'visible');
            $('#helpBox').css('visibility', 'visible');
        });
    });
    $('#boxclose4').click(function () {
        $('#boxMenu').css('visibility', 'hidden');
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#overlayOrange').fadeOut('fast');
            $('#helpBox').css('visibility', 'hidden');
            $('#helpBox').css('top', '200px');
        });
    });

    // Public channel Help Box
    $('#publicChannel').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#publicChannelBox').css('visibility', 'visible');
            $('#helpBox').css('visibility', 'hidden');
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous').click(function () {
        $('#publicChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#publicChannelBox').css('visibility', 'hidden');
            $('#publicChannelBox').css('top', '200px');
            $('#helpBox').css('visibility', 'visible');
        });
    });
    $('#boxclose5').click(function () {
        $('#boxMenu').css('visibility', 'hidden');
        $('#publicChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#publicChannelBox').css('visibility', 'hidden');
            $('#publicChannelBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // Private channel Help Box
    $('#privateChannel').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelBox').css('visibility', 'visible');
            $('#helpBox').css('visibility', 'hidden');
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous2').click(function () {
        $('#privateChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelBox').css('visibility', 'hidden');
            $('#privateChannelBox').css('top', '200px');
            $('#helpBox').css('visibility', 'visible');
        });
    });
    $('#boxclose6').click(function () {
        $('#privateChannelBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelBox').css('visibility', 'hidden');
            $('#privateChannelBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // Private channel commands Help Box
    $('#privateChannelCommands').click(function () {
        $('#helpBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelCommandsBox').css('visibility', 'visible');
            $('#helpBox').css('visibility', 'hidden');
            $('#helpBox').css('top', '200px');
        });
    });
    $('#previous3').click(function () {
        $('#privateChannelCommandsBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelCommandsBox').css('visibility', 'hidden');
            $('#privateChannelCommandsBox').css('top', '200px');
            $('#helpBox').css('visibility', 'visible');
        });
    });
    $('#boxclose7').click(function () {
        $('#privateChannelCommandsBox').animate({ 'top': '-200px' }, 500, function () {
            $('#privateChannelCommandsBox').css('visibility', 'hidden');
            $('#privateChannelCommandsBox').css('top', '200px');
            $('#overlayOrange').fadeOut('fast');
        });
    });

    // About option
    $('#aboutOption').click(function () {
        $('#helpBox').css('visibility', 'hidden');
        $('#privacyBox').css('visibility', 'hidden');
        $('#aboutBox').css('visibility', 'visible');
    });

    // Privacy option
    $('#privacyOption').click(function () {
        $('#aboutBox').css('visibility', 'hidden');
        $('#helpBox').css('visibility', 'hidden');
        $('#privacyBox').css('visibility', 'visible');
    });

    // Help option
    $('#helpOption').click(function () {
        $('#aboutBox').css('visibility', 'hidden');
        $('#privacyBox').css('visibility', 'hidden');
        $('#helpBox').css('visibility', 'visible');
    });
});