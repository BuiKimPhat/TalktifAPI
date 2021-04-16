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
        private readonly IJwtRepo _JwtRepo;

        public UserRepo(TalktifContext context,IJwtRepo jwtRepo)
        {
            _context = context;
            _JwtRepo = jwtRepo;
        }

        public ReadUserDto getInfoByEmail(string email)
        {
            var user =_context.Users.FirstOrDefault(p => p.Email == email);
            if(user!=null)
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                    Hobbies = user.Hobbies};
            throw new Exception();
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

        public SignUpRespond signUp(SignUpRequest user)
        {
            if(!isUserExists(user.Email))
            {
                if(user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                user.Password = BC.HashPassword(user.Password);
                _context.Users.Add(new User(user.Name,user.Email,user.Password,user.Gender,user.Hobbies));
                _context.SaveChanges();
                User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
                string token = _JwtRepo.GenerateSecurityToken(user.Email);
                string refreshtoken = _JwtRepo.GenerateRefreshToken(user.Email);
                _context.UserRefreshTokens.Add(new UserRefreshToken{
                        User = read.Id,
                        RefreshToken = refreshtoken,
                        CreateAt = DateTime.Now,
                        Device = user.Device
                    });
                return new SignUpRespond(new ReadUserDto{ Email = user.Email, 
                                                        Name = user.Name, 
                                                        Gender= user.Gender, 
                                                        Hobbies = user.Hobbies,
                                                        }, token,refreshtoken);
            }
            throw new Exception("Khong biet loi gi");
        }

        public LoginRespond signIn(LoginRequest user)
        {
            if(isUserExists(user.Email))
            {    if(user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
                if (true == BC.Verify(user.Password, read.Password) && read.IsActive == true){
                    string token = _JwtRepo.GenerateSecurityToken(read.Email);
                    string refreshtoken = _JwtRepo.GenerateRefreshToken(user.Email);
                    _context.UserRefreshTokens.Add(new UserRefreshToken{
                        User = read.Id,
                        RefreshToken = refreshtoken,
                        CreateAt = DateTime.Now,
                        Device = user.Device
                    });
                    return new LoginRespond(new ReadUserDto { Email = read.Email, Name = read.Name,
                                                            Id = read.Id , Gender= read.Gender, 
                                                            Hobbies = read.Hobbies, }, token,refreshtoken);
                }
                else throw new Exception();
            }
            throw new Exception();
        }

        public ReadUserDto updateInfo(UpdateInfoRequest user)
        {
            User u = _context.Users.SingleOrDefault(p => p.Email == user.Email);
            if(u != null){
                _context.Users.Update(u);
            return new ReadUserDto { Email = user.Email,
                                    Name = user.Name,
                                    Id = u.Id ,
                                    Gender= user.Gender, 
                                    Hobbies = user.Hobbies, 
                                    };
            }
            throw new Exception();
        }
        public bool inActiveUser(string email){
            if(isUserExists(email)){
                User u = _context.Users.SingleOrDefault(p => p.Email == email);
                u.IsActive = false;
                _context.Users.Update(u);
                return true;
            }
            throw new Exception("User doesn't exist!");
        }
    }
}