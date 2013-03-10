using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using UniversityChat.Model;
using UniversityChat.Data.Repositories;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;


namespace UniversityChat.Chat
{
    public class ChatHub : Hub
    {
        private LogRepository log = new LogRepository();
        private HistoryRepository history = new HistoryRepository();

        public override Task OnConnected()
        {
            logConnection(Context);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            User user = Users.GetConnectedUserByConnectionId(connectionIdGuid);
            ICollection<string> channelsUserWasIn = ChatChannels.GetRoomNamesThatUserIsConnectedTo(user);

            foreach (string channelName in channelsUserWasIn)
            {
                ChatChannels.RemoveUserFromRoom(channelName, user);
                Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "left the chat");
                Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
            }

            Users.RemoveUser(connectionIdGuid);

            Clients.All.setConnectedUserCount(Users.CountOfConnectedUsers);

            logDisconnect(Context, user);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            logReconnect(Context);

            return base.OnReconnected();
        }

        public void SetUsername(string userName)
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            Users.AddConnectedUser(connectionIdGuid, userName);
            Clients.All.setConnectedUserCount(Users.CountOfConnectedUsers);

            logSetUsername(Context, userName);
        }

        public void JoinChannel(string channelName)
        {
            User user = Users.GetConnectedUserByConnectionId(Guid.Parse(Context.ConnectionId));
            ChatChannels.AddUserToRoom(channelName, user);
            Groups.Add(Context.ConnectionId, channelName);

            Clients.Caller.setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
            Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
            Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "joined the chat");

            logJoinChannel(Context, user, channelName);
        }

        public void LeaveChannel(string channelName)
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            User user = Users.GetConnectedUserByConnectionId(connectionIdGuid);

            ChatChannels.RemoveUserFromRoom(channelName, user);
            Groups.Remove(Context.ConnectionId, channelName);

            Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "left the chat");
            Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));

            logLeaveChannel(Context, user, channelName);
        }
        
        public void Send(string channelName, string message)
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            User user = Users.GetConnectedUserByConnectionId(connectionIdGuid);
            Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, message);

            Guid roomId = ChatChannels.GetRoomByName(channelName);
            History historyItem = new History() { ConnectionId = connectionIdGuid, RoomId = roomId, LogDateTimeStamp = DateTime.Now, UserId = user.Id, Text = message};
            history.Create(historyItem);

            logSendMessage(Context, user, channelName, message);
        }

        public void GetChannelList()
        {
            Clients.Caller.setChannelList(ChatChannels.GetRoomList());
        }

        public void CreateChannel(string channelName)
        {
            ChatChannels.AddRoom(channelName);
            Clients.All.setChannelList(ChatChannels.GetRoomList());
        }

        public void DeleteChannel(string channelName)
        {
            ChatChannels.DeleteRoom(channelName);
            Clients.All.setChannelList(ChatChannels.GetRoomList());
        }

        private void logEvent(Guid connectionId, Guid userId, string message)
        {
            message = (message.Length > 199) ? message.Substring(0, 199) : message;
            log.Create(new LogMessage() { ConnectionId = connectionId, UserId = userId, Time = DateTime.Now, Message = message });
        }

        private void logConnection(HubCallerContext Context)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("Connected: ");
            logMessage.Append(Context.Headers.Get("User-Agent"));

            logEvent(Guid.Parse(Context.ConnectionId), Guid.Empty, logMessage.ToString());
        }

        private void logDisconnect(HubCallerContext Context, User user)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("Disconnected");

            logEvent(Guid.Parse(Context.ConnectionId), user.Id, logMessage.ToString());
        }

        private void logReconnect(HubCallerContext Context)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("Reconnected");

            logEvent(Guid.Parse(Context.ConnectionId), Guid.Empty, logMessage.ToString());
        }

        private void logSetUsername(HubCallerContext Context, string userName)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("Set username to: ");
            logMessage.Append(userName);
            logEvent(Guid.Parse(Context.ConnectionId), Guid.Empty, logMessage.ToString());
        }

        private void logJoinChannel(HubCallerContext Context, User user, string channelName)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("joined channel: ");
            logMessage.Append(channelName);

            logEvent(Guid.Parse(Context.ConnectionId), user.Id, logMessage.ToString());
        }

        private void logLeaveChannel(HubCallerContext Context, User user, string channelName)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("left channel: ");
            logMessage.Append(channelName);

            logEvent(Guid.Parse(Context.ConnectionId), user.Id, logMessage.ToString());
        }


        private void logSendMessage(HubCallerContext Context, User user, string channelName, string message)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append("sent message to channel: ");
            logMessage.Append(channelName);
            logMessage.Append(", ");
            logMessage.Append(message);

            logEvent(Guid.Parse(Context.ConnectionId), user.Id, logMessage.ToString());
        }
    }
}