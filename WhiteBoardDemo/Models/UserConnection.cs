using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoardDemo.Models
{
    public class UserConnection
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Guid ConnectionId { get; set; }
    }
}