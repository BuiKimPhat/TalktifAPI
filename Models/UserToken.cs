using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalktifAPI.Models
{
    public class UserToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("uid")]
        [StringLength(100)]
        public int Uid { get; set; }
        [Required]
        [Column("token")]
        [StringLength(200)]
        public string Token { get; set; }

        [ForeignKey(nameof(User))]
        [InverseProperty("UserTokenUserNavigations")]
        public virtual User UserTokenNavigation { get; set; }
    }
}