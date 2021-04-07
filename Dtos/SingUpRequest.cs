using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class SignUpRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public String Password { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        [StringLength(100)]
        public String Address { get; set; }
        [Required]
        [StringLength(200)]
        public string Hobbies { get; set; }
    }
}