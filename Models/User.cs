﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("User")]
    [Index(nameof(Email), Name = "UQ__User__AB6E6164410B6ED7", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            MessageFromNavigations = new HashSet<Message>();
            ReportReporterNavigations = new HashSet<Report>();
            UserTokenUserNavigations = new HashSet<UserToken>();
            UserFavUserNavigations = new HashSet<UserFav>();
        }
        public User(string Name,string Email,String Password)
        {
            MessageFromNavigations = new HashSet<Message>();
            ReportReporterNavigations = new HashSet<Report>();
            UserTokenUserNavigations = new HashSet<UserToken>();
            UserFavUserNavigations = new HashSet<UserFav>();
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            IsActive = false;
            IsAdmin = false;
            CreatedAt = DateTime.Now;
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(100)]
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column("createdAt", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [InverseProperty(nameof(Message.FromNavigation))]
        public virtual ICollection<Message> MessageFromNavigations { get; set; }
        [InverseProperty(nameof(Report.ReporterNavigation))]
        public virtual ICollection<Report> ReportReporterNavigations { get; set; }
        [InverseProperty(nameof(UserFav.UserNavigation))]
        public virtual ICollection<UserFav> UserFavUserNavigations { get; set; }
        [InverseProperty(nameof(UserToken.UserTokenNavigation))]
        public virtual ICollection<UserToken> UserTokenUserNavigations { get; set; }
    }
}
