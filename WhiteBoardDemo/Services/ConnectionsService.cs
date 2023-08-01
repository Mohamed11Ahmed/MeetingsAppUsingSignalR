using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteBoardDemo.Models;

namespace WhiteBoardDemo.Services
{
    public class ConnectionsService : IConnectionsService
    {
        private readonly ApplicationDbContext db;

        public ConnectionsService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void AddUserConnection(Guid guid)
        {
            var user = db.Users.FirstOrDefault(e => e.UserName == HttpContext.Current.User.Identity.Name);
            UserConnection userConnection = new UserConnection
            {
                ApplicationUser = user,
                ConnectionId = guid
            };
            db.UserConnections.Add(userConnection);
            db.SaveChanges();
        }

        public void removeUserConnection(Guid guid)
        {
            UserConnection userConnction = db.UserConnections.FirstOrDefault(e => e.ConnectionId == guid);
            if(userConnction != null)
            {
                var roomMember = db.RoomMembers.FirstOrDefault(e => e.ApplicationUserId == userConnction.ApplicationUserId);
                //db.RoomMembers.RemoveRange(db.RoomMembers.Where(e => e.ApplicationUserId == userConnction.ApplicationUserId && ));
                db.UserConnections.Remove(userConnction);
                db.SaveChanges();
            }
        }

        public IList<String> GetUserConnectionInRoom(int id)
        {

            var usersInRoom = db.RoomMembers.Where(e => e.RoomId == id).Select(e => e.ApplicationUserId).ToList();
            var connectionIds = db.UserConnections.Where(e => usersInRoom.Contains(e.ApplicationUserId)).Select(e => e.ConnectionId.ToString()).ToList();
            return connectionIds;
            
        }


    }
}