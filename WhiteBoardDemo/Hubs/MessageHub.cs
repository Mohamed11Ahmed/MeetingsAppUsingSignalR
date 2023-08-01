using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteBoardDemo.Models;
using WhiteBoardDemo.Services;

namespace WhiteBoardDemo.Hubs
{
    [HubName("Chat")]
    public class MessageHub: Hub
    {
        private readonly MessagesService _messagesService;
        private readonly IConnectionsService _connectionsService;
        public MessageHub()
        {
            _messagesService = new MessagesService(new ApplicationDbContext());
            _connectionsService = new ConnectionsService(new ApplicationDbContext());
        }
        public void sendMessage(string Message,int RoomId)
        {
            string username = HttpContext.Current.User.Identity.Name;
            _messagesService.AddMessage(Message, RoomId, username);
            var connectionIds = _connectionsService.GetUserConnectionInRoom(RoomId);
            Clients.Clients(connectionIds).newMessage(username, Message);
        }
    }
}