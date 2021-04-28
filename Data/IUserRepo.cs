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
        ReadUserDto getInfoByEmail(string email);   
        SignUpRespond signUp(SignUpRequest user);
        LoginRespond signIn(LoginRequest user);
        ReadUserDto updateInfo(UpdateInfoRequest user);
        LoginRespond RefreshToken(string token,string email);
        bool inActiveUser(string email);
        bool logOut(string token);
        List<UserChatRoom> FecthAllUserChatRoom(int userId);
        bool AddMessage(string message,int idsender,int idChatRoom);
        LoginRespond resetPass(string email,string newpass);
    }
}