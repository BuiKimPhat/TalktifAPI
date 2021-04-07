using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("User_Favs")]
    public partial class UserFav
    {
        [Column("user")]
        public int User { get; set; }
        [Column("favourite")]
        public int Favourite { get; set; }
        [Column("nickname")]
        public String NickName { get; set; }

        [Column("addedAt")]
        public DateTime AddAt { get; set; }

        [ForeignKey(nameof(User))]
        [InverseProperty("UserFavUserNavigations")]
        public virtual User UserNavigation { get; set; }
    }
}
