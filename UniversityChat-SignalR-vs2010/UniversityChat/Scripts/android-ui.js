/*  the AndroidUI object handles taking in events from the backend and 
 *  then updating the android-client UI dedicated to chat.
 */

function AndroidUI(chatDataSource) {

    var init = function (self) {
        chatDataSource.RegisterUserInterface(self);
    }

    // called from data service when hub start() is complete.
    this.HubStartDone = function () {
        Android.hubStartDone();
    };

    // called from data service when a message is received from server.
    this.BroadcastMessageToChat = function (channelName, username, message) {
        Android.broadcastMessageToChat(channelName, username, message)
    };

    // called from data service when an updated user list is received from server.
    this.SetUserList = function (channelName, userlist) {
        Android.setUserList(userlist);
    };

    // called from data service when an updated channel list is received from server.
    this.SetChannelList = function (channelList) {
        Android.setChannelList(channelList)
    };

    init(this);
}
