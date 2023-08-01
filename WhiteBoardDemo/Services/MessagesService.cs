using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhiteBoardDemo.Models;

namespace WhiteBoardDemo.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly ApplicationDbContext db;

        public MessagesService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void AddMessage(string Text, int RoomId, string Username)
        {
            var user = db.Users.FirstOrDefault(e => e.UserName == Username);
            db.Messages.Add(
                new Message { Text = Text, roomId = RoomId, ApplicationUserId = user.Id,Time = DateTime.Now }
                );
            db.SaveChanges();
        }
    }
}