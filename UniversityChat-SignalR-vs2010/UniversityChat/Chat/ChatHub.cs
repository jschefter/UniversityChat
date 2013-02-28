using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using UniversityChat.Model;



namespace UniversityChat.Chat
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            User user = Users.GetUserByConnectionId(connectionIdGuid);
            ICollection<string> channelsUserWasIn = ChatChannels.GetRoomNamesThatUserIsConnectedTo(user);

            foreach (string channelName in channelsUserWasIn)
            {
                ChatChannels.RemoveUserFromRoom(channelName, user);
                Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "left the chat");
                Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
            }

            Users.RemoveUser(connectionIdGuid);

            Clients.All.setConnectedUserCount(Users.CountOfConnectedUsers);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public void SetUsername(string userName)
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            Users.AddUser(connectionIdGuid, userName);
            Clients.All.setConnectedUserCount(Users.CountOfConnectedUsers);
        }

        public void JoinChannel(string channelName)
        {
            User user = Users.GetUserByConnectionId(Guid.Parse(Context.ConnectionId));
            ChatChannels.AddUserToRoom(channelName, user);
            Groups.Add(Context.ConnectionId, channelName);

            Clients.Caller.broadcastMessageToChat(channelName, user.NickName, "joined the chat");
            Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "joined the chat");
            Clients.Caller.setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
            Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
        }

        public void LeaveChannel(string channelName)
        {
            Guid connectionIdGuid = Guid.Parse(Context.ConnectionId);
            User user = Users.GetUserByConnectionId(connectionIdGuid);

            ChatChannels.RemoveUserFromRoom(channelName, user);
            Groups.Remove(Context.ConnectionId, channelName);

            Clients.Group(channelName).broadcastMessageToChat(channelName, user.NickName, "left the chat");
            Clients.Group(channelName).setUserList(channelName, ChatChannels.GetUsernamesInRoom(channelName));
        }
        
        public void Send(string channelName, string message)
        {
            string userName = Users.GetUserName(Guid.Parse(Context.ConnectionId));
            Clients.Group(channelName).broadcastMessageToChat(channelName, userName, message);
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
    }
}