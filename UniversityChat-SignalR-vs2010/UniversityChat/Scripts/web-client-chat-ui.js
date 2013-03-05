/*  the ChatUI object handles updating the web-client UI dedicated to chat.
*/

function WebClientChatUI($containingElement, chatDataSource) {

    var $channelList = $containingElement.find(".channel-list");
    var $chatTabs = $containingElement.find("#chat-tabs");
    var $userLists = $containingElement.find(".user-lists");
    var $addChannelButton = $containingElement.find(".add-channel");
    var $removeChannelButton = $containingElement.find(".remove-channel");
    var $messageInput = $containingElement.find("#message");
    var $sendMessageButton = $containingElement.find("#sendmessage")
    var username = "invalid";

    var init = function (self) {
        chatDataSource.RegisterUserInterface(self);

        $chatTabs.tabs({
            activate: function (event, ui) {
                // show the user list for the activated tab.
                var newTab = ui.newTab.find("a").html();

                $userLists.find("ul").each(function () {
                    $(this).removeClass("active");
                    if ($(this).hasClass(newTab)) {
                        $(this).addClass("active");
                    }
                });
            }
        });
        $chatTabs.hide();
    }

    // called from data service when hub start() is complete.
    this.HubStartDone = function () {
        $addChannelButton.removeAttr("disabled");
        $removeChannelButton.removeAttr("disabled");
        userName = $("#username").val();
    };

    function getTimeStamp() {
        var date = new Date((+new Date()));
        var h = (date.getHours() > 12) ? h = date.getHours() - 12 : h = date.getHours();
        var m = (date.getMinutes() < 10) ? m = "0" + date.getMinutes() : m = date.getMinutes();
        var s = (date.getSeconds() < 10) ? s = "0" + date.getSeconds() : s = date.getSeconds();
        var timeOfDay = (date.getHours() == h) ? "am" : "pm";
        var timeStamp = h + ':' + m + ':' + s + timeOfDay;
        return timeStamp;
    }

    // called from data service when a message is received from server.
    this.BroadcastMessageToChat = function (channelName, username, message) {

        var encodedName = $("<div />").text(username).html();
        var encodedMsg = $("<div />").text(message).html();
        var $contentDiv = $chatTabs.find(".content #" + channelName);
        $contentDiv.append("<div><strong>" + getTimeStamp() + " - " + encodedName + "</strong>:&nbsp;&nbsp;" + encodedMsg + "</div>");

        var lastChildDiv = $contentDiv.find("div:last-child");
        var lastChildDivTop = lastChildDiv.offset().top;

        var scrollTopValue = $contentDiv.height() + lastChildDivTop;       

        var currentScrollValue = $contentDiv.scrollTop() + scrollTopValue;        
        $contentDiv.scrollTop(currentScrollValue);
    };

    // called from data service when an updated user list is received from server.
    this.SetUserList = function (channelName, userlist) {
        var userHtml = [];
        $.each(userlist, function (index, username) {
            userHtml.push("<li>" + username + "</li>");
        });

        $userLists.find("." + channelName).empty().append(userHtml.join(""));
    };

    // called from data service when an updated channel list is received from server.
    this.SetChannelList = function (channelList) {

        // keep track of what channels were previously joined.
        var previouslyJoined = {};
        $channelList.find("li").each(function () {
            if ($(this).hasClass("active-channel")) {
                var channelName = $(this).html();
                previouslyJoined[channelName] = true;
            }
        });

        if (channelList.length === 0) {
            channelList = ["empty"];
        }

        // now create html for the channels in channelList.
        var channelHtml = [];
        $.each(channelList, function (index, channelname) {
            var elementClass = (previouslyJoined[channelname]) ? 'class="active-channel"' : "";
            var element = "<li " + elementClass + ">" + channelname + "</li>";
            channelHtml.push(element);
        });

        // draw the new channel list 
        $channelList.empty().append(channelHtml.join(""));

        // handle user-click to join/leave a channel
        $channelList.find("li").click(function () {
            var $this = $(this);
            var channelName = $this.html();

            if (!$this.hasClass("active-channel")) {
                // joining a channel...
                $this.addClass("active-channel");

                $(".active-chat h4").hide();
                $chatTabs.show();
                $chatTabs.find(".tabs").append('<li class="' + channelName + '"><a href="#' + channelName + '">' + channelName + '</a></li>');
                $chatTabs.find(".content").append('<div id="' + channelName + '"></div>');
                $chatTabs.tabs("refresh");

                $userLists.append("<ul class='" + channelName + "' />");

                $messageInput.removeAttr("disabled");
                $sendMessageButton.removeAttr("disabled");

                $messageInput.focus();
                chatDataSource.JoinChannel(channelName, userName);

                $chatTabs.tabs("option", "active", 0);

            } else {
                // leaving a channel.
                $this.removeClass("active-channel");

                $chatTabs.find(".tabs").find("." + channelName).remove();
                $chatTabs.find(".content").find("#" + channelName).remove();
                $chatTabs.tabs("refresh");

                $userLists.find("." + channelName).remove();

                // if no channels left, lock up UI.
                var lastChannel = ($chatTabs.find(".tabs li").length === 0) ? true : false;
                if (lastChannel) {
                    $(".active-chat h4").show();
                    $chatTabs.hide();
                    $messageInput.attr("disabled", "disabled");
                    $sendMessageButton.attr("disabled", "disabled");
                }

                chatDataSource.LeaveChannel(channelName, userName);
            }
        });
    };

    this.SetConnectedUserCount = function (userCount) {
        $("#connected-users span").html(userCount);
    };

    $("#ContentForm").submit(function (event) {
        event.stopPropagation();

        var message = $messageInput.val();
        if (message.length > 0) {
            var activeTab = $chatTabs.tabs("option", "active");
            var channelName = $chatTabs.find(".tabs li:nth-child(" + (activeTab + 1) + ") a").html();
            chatDataSource.Send(channelName, message);
        }
        $messageInput.val("").focus();

        return false;
    });

    $addChannelButton.click(function (event) {
        console.log(event);

        var channelName = window.prompt("Name of channel to create");

        if (channelName !== null) {
            // remove spaces from channel name
            var regex = new RegExp(" ", "g");
            channelName = channelName.replace(regex, "");

            if (channelName !== "") {
                chatDataSource.CreateChannel(channelName);
            }
        }
    });

    $removeChannelButton.click(function () {
        var channelName = window.prompt("Name of channel to remove");

        if (channelName !== null && channelName !== "") {
            chatDataSource.DeleteChannel(channelName);
        }
    });

    init(this);
}
