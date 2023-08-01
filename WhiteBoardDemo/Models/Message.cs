using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoardDemo.Models
{
    public class Message
    {
        public int  Id { get; set; }

        public string Text { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int roomId { get; set; }
        public virtual Room Room { get; set; }


        public DateTime Time { get; set; }
    }
}