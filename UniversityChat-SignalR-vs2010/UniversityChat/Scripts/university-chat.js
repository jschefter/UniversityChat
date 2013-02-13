$(function () {
    var $chat = $("#chat");

    var joinChat = function () {
        var chat = $.connection.chatHub;

        chat.client.broadcastMessageToChat = function (name, message) {
            var encodedName = $("<div />").text(name).html();
            var encodedMsg = $("<div />").text(message).html();
            $chat.append("<div><strong>" + encodedName + "</strong>:&nbsp;&nbsp;" + encodedMsg + "</div>");

            $chat.animate({
                scrollTop: $chat.find("div:last-child").offset().top
            }, 250);
        };

        chat.client.setUserList = function (userlist) {
            var userHtml = [];
            $.each(userlist, function (index, username) {
                userHtml.push("<li>" + username + "</li>");
            });

            $(".userlist").empty().append(userHtml.join(""));
        };

        var name = $("#username").val();
        $("#displayname").val(name);
        $("#message").focus();

        $.connection.hub.start().done(function () {
            chat.server.newUser(name);

            $("#chat-form").submit(function () {
                var message = $("#message").val();
                if (message.length > 0) {
                    chat.server.send($("#displayname").val(), $("#message").val());
                }
                $("#message").val("").focus();
                return false;
            });
        });
    }

    $("#name-form").submit(function () {
        $("#username, #joinchat").attr("disabled", "disabled");
        $("#message, #sendmessage").removeAttr("disabled");
        joinChat();
        return false;
    });
});