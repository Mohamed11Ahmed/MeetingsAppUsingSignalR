using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhiteBoardDemo.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name ="Room Name")]
        public string RoomName { get; set; }

        public byte[] WhiteboardData { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class RoomMember
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }


        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}