using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhiteBoardDemo.Models;
using WhiteBoardDemo.Services;

namespace WhiteBoardDemo.Hubs
{
    [HubName("whiteboard")]
    public class WhiteboardHub : Hub
    {
        private readonly IConnectionsService _connectionsService;
        private readonly IRoomsService _roomsService;
        public WhiteboardHub()
        {
            _connectionsService = new ConnectionsService(new ApplicationDbContext());
            _roomsService = new RoomsService(new ApplicationDbContext());
        }

        public void DrawLine(int startX, int startY, int endX, int endY,bool isdrawing,string fontColor, int fontStroke,int roomId)
        {
            var connectionIds = _connectionsService.GetUserConnectionInRoom(roomId);
            Clients.Clients(connectionIds).drawLine(startX, startY, endX, endY,isdrawing,fontColor, fontStroke);
        }
        public void saveWhiteboard(int roomId,string canvasDataUrl)
        {
            _roomsService.saveWhiteboard(roomId,canvasDataUrl);
            Clients.All.loadWhiteboard(canvasDataUrl);
        }

  

        public override Task OnConnected()
        {
            _connectionsService.AddUserConnection(Guid.Parse(Context.ConnectionId));
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // _connectionsService.removeUserConnection(Guid.Parse(Context.ConnectionId));
            return base.OnDisconnected(stopCalled);
        }

    }
}


