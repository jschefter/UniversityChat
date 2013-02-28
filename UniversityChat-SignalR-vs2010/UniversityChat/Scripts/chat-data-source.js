/*  The ChatDataSource is the interface between the chatUI and the back-end service.
*/

function ChatDataSource() {
    var chat = $.connection.chatHub;
    var userInterfaces = [];        // there should only ever be one userInterface in this array.

    chat.client.broadcastMessageToChat = function (channelName, userName, message) {
        $.each(userInterfaces, function (index, userInterface) {
            userInterface.BroadcastMessageToChat(channelName, userName, message);
        });
    };

    chat.client.setUserList = function (channelName, userList) {
        $.each(userInterfaces, function (index, userInterface) {
            userInterface.SetUserList(channelName, userList);
        });
    };

    chat.client.setChannelList = function (channelList) {
        $.each(userInterfaces, function (index, userInterface) {
            userInterface.SetChannelList(channelList);
        });
    };

    chat.client.setConnectedUserCount = function (userCount) {
        $.each(userInterfaces, function (index, userInterface) {
            userInterface.SetConnectedUserCount(userCount);
        });
    };

    var hubStartDone = function () {
        $.each(userInterfaces, function (index, userInterface) {
            userInterface.HubStartDone();
        });
    };

    this.RegisterUserInterface = function (userInterface) {
        userInterfaces.push(userInterface);
    };

    // called from main script when user has submitted their login credentials.
    this.StartHub = function (userName) {
        $.connection.hub.start().done(function () {
            chat.server.setUsername(userName);
            hubStartDone();
            chat.server.getChannelList();
        });
    }

    // called from UI when user wants to join a chat channel.
    this.JoinChannel = function (channelName, userName) {
        chat.server.joinChannel(channelName);
    };

    // called from UI when user wants to leave a chat channel.
    this.LeaveChannel = function (channelName, userName) {
        chat.server.leaveChannel(channelName);
    };

    // called from UI when user wants to send a message to a channel.
    this.Send = function (channelName, userName, message) {
        chat.server.send(channelName, message);
    };

    // called from UI when user wants to create a new channel.
    this.CreateChannel = function (channelName) {
        chat.server.createChannel(channelName);
    };

    // called form UI when user wants to delete a channel.
    this.DeleteChannel = function (channelName) {
        chat.server.deleteChannel(channelName);
    };
}