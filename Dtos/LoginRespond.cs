using System;
using System.ComponentModel.DataAnnotations;
using TalktifAPI.Models;

namespace TalktifAPI.Dtos
{
    public class LoginRespond
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
        [Required]
        public string Token {get; set;}
        public LoginRespond(ReadUserDto user , string token)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Gender = user.Gender;
            Address= user.Address;
            Hobbies = user.Hobbies;
            Token = token;
        }
    }
}