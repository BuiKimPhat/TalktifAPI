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
        CreateUserDto signUp(CreateUserDto user);
        User signin();
    }
}