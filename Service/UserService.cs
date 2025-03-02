using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.User;
using TalktifAPI.Middleware;
using TalktifAPI.Models;
using TalktifAPI.Repository;
using BC = BCrypt.Net.BCrypt;

namespace TalktifAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userService;
        private readonly IUserRefreshTokenRepository _tokenService;
        private readonly IJwtService _jwtService;
        private readonly JwtConfig _JwtConfig;
        private readonly IReportRepository _reportRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public UserService(IUserRepository userService,IUserRefreshTokenRepository tokenService,ICityRepository cityRepository,
                    IJwtService jwtService, IOptions<JwtConfig> JwtConfig,IReportRepository reportRepository, ICountryRepository countryRepository)
        {
            _userService = userService;
            _tokenService = tokenService;
            _jwtService = jwtService;
            _JwtConfig = JwtConfig.Value;
            _reportRepository = reportRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }
        public ReadUserDto getInfoById(int id)
        {
            User user = _userService.GetById(id);
            if(user==null) throw new Exception("user doesn't exist!");
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,IsAdmin = user.IsAdmin, 
                                    IsActive = user.IsActive, CityId = user.CityId, Hobbies = user.Hobbies};
        }

        public bool inActiveUser(int id)
        {
            User u = _userService.GetById(id);
            if(u==null) return false;
            u.IsActive = false;
            return true;
        }

        public bool isUserExists(int id)
        {
            User u = _userService.GetById(id);
            if(u==null) return false;
            return true;
        }

        public RefreshTokenRespond RefreshToken(ReFreshToken token,int id)
        {
            try{
            if(!_jwtService.ValidRefreshToken(token.RefreshToken)) throw new SecurityTokenExpiredException();  
            var jwtToken = _jwtService.GenerateSecurityToken(_jwtService.GetId(token.RefreshToken));
            return new RefreshTokenRespond{
                Token = jwtToken
            };
            }catch(Exception e){
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool Report(ReportRequest request)
        {
            _reportRepository.Insert(new Report{
                Note = request.Note,
                Reporter = request.Reporter,
                Suspect = request.Suspect,
                Reason = request.Reason,
                CreatedAt = DateTime.Now.AddHours(7),
                Status = false,
            });
            return true;
        }

        public LoginRespond resetPass(string email, string newpass)
        {
            User user = _userService.GetUserByEmail(email);
            if(user == null){
                throw new Exception("Wrong email");
            }
            user.Password = BC.HashPassword(newpass);
            _userService.Update(user);
            var jwtToken = _jwtService.GenerateRefreshToken(user.Id);
            _tokenService.Insert(new UserRefreshToken{
                    User = user.Id,RefreshToken = jwtToken,
                    CreateAt = DateTime.Now.AddHours(7),Device = "Reset Password"});
            return new LoginRespond(getInfoById(user.Id), jwtToken);
        }

        public LoginRespond signIn(LoginRequest user)
        { 
            User read = _userService.GetUserByEmail(user.Email);
            if(read==null) throw new Exception("Wrong Email");
            if (true == BC.Verify(user.Password, read.Password) && read.IsActive == true){
                string token = _jwtService.GenerateRefreshToken(read.Id);
                _tokenService.Insert(new UserRefreshToken{
                User = read.Id,RefreshToken = token,
                CreateAt = DateTime.Now.AddHours(7),Device = user.Device});
                return new LoginRespond(new ReadUserDto { Email = read.Email, Name = read.Name,
                                        Id = read.Id , Gender= read.Gender, IsAdmin = read.IsAdmin, 
                                        CityId = read.CityId , IsActive = read.IsActive, Hobbies = read.Hobbies }, token);
            }
            else{
                if(read.IsActive!=true) throw new Exception("Your account are locked, contact to admin for infomation");
            }
            throw new Exception("Wrong Password");
        }

        public SignUpRespond signUp(SignUpRequest user)
        {
            User read = _userService.GetUserByEmail(user.Email);
            if(read!=null||IsValidEmail(user.Email)) throw new Exception("User has already exist"); 
            _userService.Insert(new User(user.Name,user.Email,BC.HashPassword(user.Password),
                                user.Gender,user.CityId,false,user.Hobbies));
            read = _userService.GetUserByEmail(user.Email);
            string token = _jwtService.GenerateRefreshToken(read.Id);
            _tokenService.Insert(new UserRefreshToken{
                User = read.Id,RefreshToken = token,
                CreateAt = DateTime.Now.AddHours(7),Device = user.Device});
            return new SignUpRespond(new ReadUserDto{ Id = read.Id, Email = user.Email,IsActive = read.IsActive,
                                        Name = user.Name,IsAdmin = read.IsAdmin, 
                                        Gender= user.Gender, CityId = user.CityId, Hobbies = user.Hobbies },token);
        }
        public ReadUserDto updateInfo(UpdateInfoRequest user)
        {
            if(user==null || !isUserExists(user.Id)) throw new Exception("Not found");
            User u = _userService.GetById(user.Id);
            if(!BC.Verify(user.OldPassword,u.Password)) throw new Exception("Wrong Password");
            u.Email = user.Email;
            u.Gender = user.Gender;
            u.Name = user.Name;
            u.Password = BC.HashPassword(user.Password);
            u.CityId = user.CityId;
            u.Hobbies = user.Hobbies;
            _userService.Update(u);
            return new ReadUserDto { Email = user.Email,Name = user.Name,Id = u.Id ,Gender= user.Gender,
                IsAdmin = u.IsAdmin, CityId = user.CityId, IsActive = u.IsActive,Hobbies=u.Hobbies };
        }

        public bool CheckToken(string token, int id)
        {
            UserRefreshToken t = _tokenService.GetById(id);
            if( t!= null && String.Compare(t.RefreshToken,token)==0) return true;
            return false;
        }

        public ReadUserDto getInfoByEmail(string email)
        {
            User user = _userService.GetUserByEmail(email);
            if(user==null) throw new Exception("user doesn't exist!");
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                            IsAdmin = user.IsAdmin, IsActive = user.IsActive, CityId = user.CityId, Hobbies = user.Hobbies};
        }

        public List<Country> GetAllCountry()
        {
            return _countryRepository.GetAll();
        }

        public List<City> GettCityByCountry(int countryid)
        {
            return _cityRepository.GetCityByCountry(countryid);
        }
        bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
                }   
        }
    }   
}