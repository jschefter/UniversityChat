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
            string username = ConnectedUsers.RemoveUser(Context.ConnectionId);
            Clients.All.broadcastMessageToChat(username, "left the chat");
            Clients.All.setUserList(ConnectedUsers.GetConnectedUsers());
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public void Send(string username, string message)
        {
            Clients.All.broadcastMessageToChat(username, message);
        }

        public void NewUser(string username)
        {
            ConnectedUsers.AddUser(Context.ConnectionId, username);
            Clients.All.broadcastMessageToChat(username, "joined the chat");
            Clients.All.setUserList(ConnectedUsers.GetConnectedUsers());
        }
    }
}