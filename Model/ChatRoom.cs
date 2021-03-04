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
        public DateTime createdAT { get; set; }
        
        
        [Required]
        [MaxLength(100)]
        public string roomName { get; set; }
        
    }
}