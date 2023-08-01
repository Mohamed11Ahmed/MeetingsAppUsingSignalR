using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoardDemo.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public string RoomName { get; set; }

        public string WhiteboardData { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}