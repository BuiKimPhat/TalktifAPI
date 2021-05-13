using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalktifAPI.Dtos;
using TalktifAPI.Middleware;
using TalktifAPI.Models;
using BC = BCrypt.Net.BCrypt;

namespace TalktifAPI.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly TalktifContext _context;
        private readonly IJwtRepo _JwtRepo;
        private readonly JwtConfig _JwtConfig;

        public UserRepo(TalktifContext context,IJwtRepo jwtRepo, IOptions<JwtConfig> JwtConfig)
        {
            _context = context;
            _JwtRepo = jwtRepo;
            _JwtConfig = JwtConfig.Value;
        }

        public ReadUserDto getInfoById(int id)
        {
            var user =_context.Users.FirstOrDefault(p => p.Id == id);
            if(user!=null)
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                    Hobbies = user.Hobbies};
            throw new Exception();
        }

        public bool isUserExists(int id)
        {
            User u = _context.Users.FirstOrDefault(p => p.Id == id);
            if(u==null) return false;
            return true;
        }

        public bool saveChange()
        {
            return (_context.SaveChanges() >= 0);
        }

        public SignUpRespond signUp(SignUpRequest user)
        {
            if(!isUserExists(1))
            {
                if(user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                user.Password = BC.HashPassword(user.Password);
                _context.Users.Add(new User(user.Name,user.Email,user.Password,user.Gender,user.Hobbies));
                _context.SaveChanges();
                User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
                string token = _JwtRepo.GenerateSecurityToken(read.Id);
                string refreshtoken = _JwtRepo.GenerateRefreshToken(read.Id);
                _context.UserRefreshTokens.Add(new UserRefreshToken{
                        User = read.Id,
                        RefreshToken = refreshtoken,
                        CreateAt = DateTime.Now,
                        Device = user.Device
                    });
                // return new SignUpRespond(new ReadUserDto{ Id = read.Id,
                //                                         Email = user.Email, 
                //                                         Name = user.Name,
                //                                         IsAdmin = read.IsAdmin, 
                //                                         Gender= user.Gender, 
                //                                         Hobbies = user.Hobbies,
                //                                         }, token,refreshtoken);
                return null;
            }
            throw new Exception("Khong biet loi gi");
        }

        public LoginRespond signIn(LoginRequest user)
        {
            if(isUserExists(1))
            {    if(user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                User read = _context.Users.FirstOrDefault(p => p.Email == user.Email);
                if (true == BC.Verify(user.Password, read.Password) && read.IsActive == true){
                    string token = _JwtRepo.GenerateSecurityToken(read.Id);
                    string refreshtoken = _JwtRepo.GenerateRefreshToken(read.Id);
                    _context.UserRefreshTokens.Add(new UserRefreshToken{
                        User = read.Id,
                        RefreshToken = refreshtoken,
                        CreateAt = DateTime.Now,
                        Device = user.Device
                    });
                    // return new LoginRespond(new ReadUserDto { Email = read.Email, Name = read.Name,
                    //                                         Id = read.Id , Gender= read.Gender, IsAdmin = read.IsAdmin, 
                    //                                         Hobbies = read.Hobbies, }, token,refreshtoken);
                    return null;
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
            return new ReadUserDto { Email = user.Email,Name = user.Name,
                                    Id = u.Id ,Gender= user.Gender, IsAdmin = u.IsAdmin,  Hobbies = user.Hobbies, };
            }
            throw new Exception();
        }
        public bool inActiveUser(int id){
            if(isUserExists(id)){
                User u = _context.Users.SingleOrDefault(p => p.Id == id);
                u.IsActive = false;
                _context.Users.Update(u);
                return true;
            }
            throw new Exception("User doesn't exist!");
        }
        private bool ValidRefreshToken(string token,string email){
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_JwtConfig.secret2);
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                string mail = jwtToken.Claims.First(claim => claim.Type == "Email").Value;
                if(email != mail) throw new Exception();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);
                return true;
            }catch(Exception){
                return false;
            }
        }
        public RefreshTokenRespond RefreshToken(string token,string email)
        {
            UserRefreshToken record = _context.UserRefreshTokens.FirstOrDefault(u => u.RefreshToken==token);
            if (record == null) throw new Exception("Invalid Token");
            if(ValidRefreshToken(token,email)) throw new SecurityTokenExpiredException();
            record.RefreshToken = _JwtRepo.GenerateRefreshToken(record.User);   
            _context.UserRefreshTokens.Update(record);
            var jwtToken = _JwtRepo.GenerateSecurityToken(record.User);
            return new RefreshTokenRespond{
                Token = jwtToken
            };
        }
        public LoginRespond resetPass(string email, string newpass)
        {
            User user = _context.Users.SingleOrDefault(u => u.Email == email);
            if(user == null){
                throw new Exception("Wrong email");
            }
            user.Password = BC.HashPassword(newpass);
            _context.Users.Update(user);
            var jwtToken = _JwtRepo.GenerateSecurityToken(user.Id);

            //return new LoginRespond(getInfoById(user.Id), jwtToken, null);
            return null;
        }

        public bool isUserExists(string user)
        {
            throw new NotImplementedException();
        }
    }
}