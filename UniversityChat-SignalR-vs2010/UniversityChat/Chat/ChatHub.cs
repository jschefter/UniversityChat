using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace UniversityChat
{
    public class ChatHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string[] channelsUserWasIn = ChatChannels.GetChannels(Context.ConnectionId);

            foreach (string channelName in channelsUserWasIn)
            {
                string userName = ChatChannels.RemoveUser(channelName, Context.ConnectionId);
                Clients.Group(channelName).broadcastMessageToChat(channelName, userName, "left the chat");
                Clients.Group(channelName).setUserList(channelName, ChatChannels.GetConnectedUsers(channelName));
            }
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public void JoinChannel(string channelName, string userName)
        {
            ChatChannels.AddUser(channelName, Context.ConnectionId, userName);
            Groups.Add(Context.ConnectionId, channelName);
            Clients.Caller.broadcastMessageToChat(channelName, userName, "joined the chat");
            Clients.Group(channelName).broadcastMessageToChat(channelName, userName, "joined the chat");
            Clients.Caller.setUserList(channelName, ChatChannels.GetConnectedUsers(channelName));
            Clients.Group(channelName).setUserList(channelName, ChatChannels.GetConnectedUsers(channelName));
        }

        public void LeaveChannel(string channelName, string userName)
        {
            // TODO: handle user leaving an open channel
        }
        
        public void Send(string channelName, string userName, string message)
        {
            Clients.Group(channelName).broadcastMessageToChat(channelName, userName, message);
        }

        public void GetChannelList()
        {
            Clients.Caller.setChannelList(ChatChannels.GetChannelList());
        }

        public void CreateChannel(string channelName)
        {
            ChatChannels.AddChannel(channelName);
            Clients.All.setChannelList(ChatChannels.GetChannelList());
        }

        public void DeleteChannel(string channelName)
        {
            ChatChannels.DeleteChannel(channelName);
            Clients.All.setChannelList(ChatChannels.GetChannelList());
        }
    }
}