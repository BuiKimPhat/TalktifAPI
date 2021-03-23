using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Dtos
{
    public class LoginUserDto {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}