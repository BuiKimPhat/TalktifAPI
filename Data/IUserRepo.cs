using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public interface IUserRepo
    {
        bool saveChange();
        bool isUserExists(string user);
        ReadUserDto getInfoById(int id);   
        SignUpRespond signUp(SignUpRequest user);
        LoginRespond signIn(LoginRequest user);
        ReadUserDto updateInfo(UpdateInfoRequest user);
        RefreshTokenRespond RefreshToken(string token,string email);
        bool inActiveUser(int id);
        LoginRespond resetPass(string email,string newpass);
    }
}