using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TalktifAPI.Models
{
    [Table("Message")]
    public partial class Message
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("from")]
        public int From { get; set; }
        [Column("to")]
        public int To { get; set; }
        [Required]
        [Column("content")]
        [StringLength(1000)]
        public string Content { get; set; }
        [Column("sentAt", TypeName = "datetime")]
        public DateTime? SentAt { get; set; }

        [ForeignKey(nameof(From))]
        [InverseProperty(nameof(User.MessageFromNavigations))]
        public virtual User FromNavigation { get; set; }
        [ForeignKey(nameof(To))]
        [InverseProperty(nameof(User.MessageToNavigations))]
        public virtual User ToNavigation { get; set; }
    }
}
