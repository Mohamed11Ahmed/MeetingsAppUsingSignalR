using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoardDemo.Models;

namespace WhiteBoardDemo.Services
{
    public interface IRoomsService
    {
        void CreateRoom(Room room);

        IEnumerable<Room> GetAllRooms();

        Room GetRoomById(int? id);

        IEnumerable<Message> GetRoomMessages(int? id);

        void AddRoomMember(Room room,string Username);
        void LeaveRoom(int? roomId,string Username);

        void RemoveRoom(int id);

        void saveWhiteboard(int RoomId, string DataUrl);

    }
}
