$(document).ready(function () {
    var chatDataSource = new ChatDataSource();
    var chatUI = new ChatUI($(".chat-container"), chatDataSource);

    $("#login-form").submit(function () {
        chatDataSource.StartHub();
        $("#username, #sign-in-button").attr("disabled", "disabled");
        return false;
    });
});

