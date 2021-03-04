using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Model
{
    public class User
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }

        [Required]
        public int isAdmin { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

    }
}