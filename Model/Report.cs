using System;
using System.ComponentModel.DataAnnotations;

namespace TalktifAPI.Model
{
    public class Report
    {
        [Key]
        [Required]
        public int rpId { get; set; }

        [Required]
        public User rpTer { get; set; }
        
        [Required]
        public int rpterId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string rpCategory { get; set; }
        
        [Required]
        [MaxLength(1000)]
        public string rpDescription { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string rpStatus { get; set; }
        
        [Required]
        public DateTime creatAt { get; set; }
            
        
    }
}