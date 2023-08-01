using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteBoardDemo.Models;

namespace WhiteBoardDemo.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext db;

        public RoomsService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateRoom(Room room)
        {
            var user = db.Users.FirstOrDefault(e => e.UserName == HttpContext.Current.User.Identity.Name);
            room.ApplicationUserId = user.Id;
            db.Rooms.Add(room);
            db.RoomMembers.Add(new RoomMember { Room = room, ApplicationUser = user });
            db.SaveChanges();
        }
   
        public IEnumerable<Room> GetAllRooms()
        {
            return db.Rooms.ToList();
        }

        public Room GetRoomById(int? id)
        {
            return db.Rooms.Find(id);
        }

        public IEnumerable<Message> GetRoomMessages(int? id)
        {
            return db.Messages.Where(e => e.roomId == id).ToList().OrderBy(e => e.Time);
        }

        public void AddRoomMember(Room room, string Username)
        {
            var user = db.Users.FirstOrDefault(e => e.UserName == Username);
            db.RoomMembers.Add(new RoomMember { Room = room, ApplicationUser = user });
            db.SaveChanges();
        }
        public void LeaveRoom(int? roomId, string Username)
        {
            var user = db.Users.FirstOrDefault(e => e.UserName == Username);
            db.RoomMembers.RemoveRange(db.RoomMembers.Where(e => e.ApplicationUserId == user.Id && e.RoomId == roomId));
            db.SaveChanges();
        }

        public void RemoveRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
        }

        public void saveWhiteboard(int RoomId, string DataUrl)
        {
            Room room = db.Rooms.Find(RoomId);
            if (room != null)
            {
                byte[] ImageData = Convert.FromBase64String(DataUrl.Split(',')[1]);
                room.WhiteboardData = ImageData;
                db.SaveChanges();
            }
        }
    }
}