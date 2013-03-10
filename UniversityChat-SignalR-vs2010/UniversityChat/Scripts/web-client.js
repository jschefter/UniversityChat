$(document).ready(function () {
    var $username = $(".username");

    // if username is defined, that means the user is logged in.
    if ($username.length > 0) {
        var chatDataSource = new ChatDataSource();
        var chatUI = new WebClientChatUI($(".chat-container"), chatDataSource);

        var username = $username.html();
        chatDataSource.StartHub(username);
    }
});

