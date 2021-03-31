using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class ReadUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public bool Gender { get; set; }
        public String Address { get; set; }
        public string Hobbies { get; set; }
    }
}