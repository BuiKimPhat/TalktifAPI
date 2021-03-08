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
        [Key]
        [Column("user")]
        public int User { get; set; }
        [Key]
        [Column("favourite")]
        public int Favourite { get; set; }

        [ForeignKey(nameof(Favourite))]
        [InverseProperty("UserFavFavouriteNavigations")]
        public virtual User FavouriteNavigation { get; set; }
        [ForeignKey(nameof(User))]
        [InverseProperty("UserFavUserNavigations")]
        public virtual User UserNavigation { get; set; }
    }
}
