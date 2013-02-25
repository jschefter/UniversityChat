﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UniversityChat.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>University Chat</title>

    <link href="Content/site.css" rel="stylesheet" type="text/css" />
    <link href="Content/south-street/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" type="text/css" />
    
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-1.0.0-rc2.js" type="text/javascript"></script>
    <script src="/signalr/hubs" type="text/javascript"></script>   <!-- autogenerated by SignalR -->

    <script src="Scripts/chat-data-source.js" type="text/javascript"></script>
    <script src="Scripts/chat-ui.js" type="text/javascript"></script>
    <script src="Scripts/web-client.js" type="text/javascript"></script>

    <script src="http://use.edgefonts.net/poiret-one.js"></script>  <!-- font: poiret-one -->

</head>
<body>
    
    <div class="container">
        <div class="header">
            <h1><a href="">University Chat</a></h1>
            <div class="login-container">
                <form action="" id="login-form">
                    <div>
                        <input type="text" size="25" id="username" name="username" 
                            placeholder="Username" /><br />
                        <button id="signUp">Sign Up</button>
                    </div>
                    <div>
                        <input type="password" size="25" id="password" name="password" 
                            placeholder="Password" /><br />
                        <a href="">Forgot Password?</a>
                    </div>
                    <div>
                        <button id="sign-in-button" type="submit">Sign In</button><br />
                    </div>
                </form>
            </div>
        </div>
        <div class="chat-container">
            <div class="channels">
                <h4>Public Channels</h4>
                <ul class="channel-list">
                </ul>
                <button class="add-channel" disabled="disabled">Create a Channel</button>
                <button class="remove-channel" disabled="disabled">Remove a Channel</button>
            </div>
            <div class="active-chat">
                <h4>Click on a public channel to the left to join a chat room</h4>

                <div id="chat-tabs">
                    <ul class="tabs">
                        
                    </ul>
                    <div class="content">
                        
                    </div>
                </div>

                <form action="" id="chat-form">
                    <input type="text" id="message" disabled="disabled"/>
                    <input type="submit" id="sendmessage" value="Send" disabled="disabled" />
                    <input type="hidden" id="displayname" />
                </form>
            </div>
            <div class="users">
                <h4>Connected Users</h4>
                <div class="user-lists"></div>
            </div>
            
        </div>
        <div class="footer">
            <a id="about" style="cursor: pointer; color: crimson;">About</a> |
            <a id="privacy" style="cursor: pointer; color: teal;">Privacy & Terms</a> |
            <a href="https://github.com/jschefter/UniversityChat" style="color: darksalmon;" target="_blank">Github</a> |
            <a id="help" style="cursor: pointer; color: #8fbc8f;">Help</a>
        </div>
    </div>
    
    <!-- Image links -->
    <!--<div class="content">
        <a class="welcome" id="welcome"></a>
        <a class="feedback" id="feedback"></a>
    </div>-->

    <!-- The overlays -->
    <div class="overlay" id="overlayOrange" style="display:none; background-color: #f4a460;"></div>
    <div class="overlay" id="overlayLB" style="display:none; background-color: #8fbc8f;" ></div>
    
    <!-- Description Boxes -->
    <div class="box" id="welcomeBox">
        <a class="boxclose" id="boxclose"></a>
        <h1>Welcome</h1>
        <p>
            Welcome to University Chat!
        </p>
    </div>

    <div class="box" id="aboutBox">
        <a class="boxclose" id="boxclose2"></a>
        <h1>About University Chat</h1>
        <p>
            University Chat was developed to ...
        </p>
    </div>

    <div class="box" id="privacyBox">
        <a class="boxclose" id="boxclose3"></a>
        <h1>Privacy & Terms</h1>
        <p>
            Privacy Page
        </p>
    </div>
    
    <div class="box" id="helpBox">
        <a class="boxclose" id="boxclose4"></a>
        <h1>Help</h1>
        <p id="publicChannel" style="cursor: pointer; color: mediumslateblue;">How to create a public channel</p>
        <p id="privateChannel" style="cursor: pointer; color: mediumvioletred;">How to create a private channel</p>
        <p id="privateChannelCommands" style="cursor: pointer; color: mediumturquoise;">Private channel commands</p>
    </div>
    
    <div class="box" id="publicChannelBox">
        <a class="boxclose" id="boxclose5"></a>
        <h1>How to create a public channel</h1>
        <p>1. Do this</p>
        <p>2. Do that</p>
        <p id="previous" style="cursor: pointer; color: crimson">Go to previous</p>
    </div>
    
    <div class="box" id="privateChannelBox">
        <a class="boxclose" id="boxclose6"></a>
        <h1>How to create a private channel</h1>
        <p>1. Do this</p>
        <p>2. Do that</p>
        <p id="previous2" style="cursor: pointer; color: crimson">Go to previous</p>
    </div>
    
    <div class="box" id="privateChannelCommandsBox">
        <a class="boxclose" id="boxclose7"></a>
        <h1>Private channel commands</h1>
        <p>Kick</p>
        <p>Invite</p>
        <p>Delegate</p>
        <p id="previous3" style="cursor: pointer; color: crimson">Go to previous</p>
    </div>
    
    <!-- Scripts -->
    <script type="text/javascript">
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
                $('#publicChannelBox').animate({ 'top': '-200px' }, 500, function() {
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
                var myWindow = window.open('', '', 'width=500, height=500');
                myWindow.document.write("<p>Registration Page</p>");
                myWindow.document.write("<p>On Progress..</p>");
                myWindow.focus();
            });
        });
    </script>

</body>
</html>