using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                    Address = user.Address,Hobbies = user.Hobbies};
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
            _context.Users.Add(new User(user.Name,user.Email,user.Password,user.Gender,user.Hobbies,user.Address));
            return user;
        }

        public ReadUserDto signIn(LoginUserDto user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
            if (true == BC.Verify(user.Password, read.Password) && read.IsActive == true)
                return new ReadUserDto { Email = read.Email, Name = read.Name, Id = read.Id };
            else return new ReadUserDto { Id = 0, Name = "Nguoi dung Talktif", Email = "SignUpFailed@mail.com" };
        }

        public List<ReadUserDto> getAllUser()
        {
            List<ReadUserDto> list = new List<ReadUserDto>();
            try{
            var read = _context.Users.Where(p => p.Id != 0);
            foreach(var u in read){
                list.Add(new ReadUserDto{
                    Email =  u.Email, Name =  u.Name, Id =  u.Id 
                });
            }
            } catch(Exception err){
                Console.Write(err.ToString());
            }
            return list;
        }
    }
}