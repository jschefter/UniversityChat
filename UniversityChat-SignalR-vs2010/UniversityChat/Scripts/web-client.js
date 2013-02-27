$(document).ready(function () {
    var chatDataSource = new ChatDataSource();
    var chatUI = new ChatUI($(".chat-container"), chatDataSource);

    $("#login-form").submit(function () {
        // TODO: validate username & password here before starting hub...
        var username = $("#username").val();

        if (username.length === 0) {
            // username is empty
            window.alert("Invalid username");
            $("#username").focus();
        } else {
            // We want to verify user exists in DB, based on their info. Let's check it.
            // How do we go from pure JS to safely/securely doing this?
            chatDataSource.StartHub();
            $("#username, #password, #sign-in-button").attr("disabled", "disabled");
        }
        return false;
    });
});

