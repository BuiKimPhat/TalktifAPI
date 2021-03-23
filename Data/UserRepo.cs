using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using BC = BCrypt.Net.BCrypt;

namespace TalktifAPI.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly TalktifContext _context;

        public UserRepo(TalktifContext context)
        {
            _context = context;
        }

        public ReadUserDto getInfoByEmail(string email)
        {
            User user =_context.Users.FirstOrDefault(p => p.Email == email);
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id};
        }

        public bool isUserExists(string user)
        {
            User u = _context.Users.FirstOrDefault(p => p.Email == user);
            if(u==null) return false;
            return true;
        }

        public bool saveChange()
        {
            return (_context.SaveChanges() >= 0);
        }

        public CreateUserDto signUp(CreateUserDto user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Password = BC.HashPassword(user.Password);
            _context.Users.Add(new User(user.Name,user.Email,user.Password));
            return user;
        }

        public ReadUserDto signIn(LoginUserDto user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
            if(true == BC.Verify(user.Password,read.Password))
            return new ReadUserDto{Email = read.Email, Name = read.Name,Id = read.Id};
            else return new ReadUserDto{Id = 0, Name = "Nguoi dung Talktif",Email="SignUpFailed@mail.com"};
        }
    }
}