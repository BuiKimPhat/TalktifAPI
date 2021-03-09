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
    }
}