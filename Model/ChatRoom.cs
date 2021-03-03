using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Model
{
    public class ChatRoom
    {
        [Key]
        [Required]
        public int roomId { get; set; }
        
        [Required]
        public DateTime CreateAT { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string roomCategory { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string RoomName { get; set; }
        
    }
}