using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalktifAPI.Model
{
    public class Messages
    {
        [Key]
        [Required]
        public int  mesId { get; set; }

        [Required]
        public User sender { get; set; }

        [Required]
        [ForeignKey("sender")]
        public int senderId { get; set; }

        public ChatRoom chatRoom { get; set; }
        
        [Required]
        [ForeignKey("chatRoom")]
        public int roomId { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string content { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string contentCategory { get; set; }
        
        [Required]
        public DateTime createdAt { get; set; } 
    }
}