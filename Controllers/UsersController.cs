using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using System.Text.Json;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]  
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository)
        {
            _repository = repository;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public ActionResult<ReadUserDto> signUp(CreateUserDto user)
        {
            if(_repository.isUserExists(user.Email)) return new ReadUserDto{Id = 0, Name = "Nguoi dung Talktif",Email="SignUpFailed@mail.com"};
            CreateUserDto u = _repository.signUp(user);
            _repository.saveChange();
            return Ok(_repository.getInfoByEmail(user.Email));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<ReadUserDto> signIn(LoginUserDto user)
        {
            if(!_repository.isUserExists(user.Email)) return new ReadUserDto{Id = 0, Name = "Nguoi dung Talktif",Email="SignUpFailed@mail.com"};
            return Ok(_repository.signIn(user));
        }
        [HttpGet]
        [Route("{email}")]
        public ActionResult<ReadUserDto> getUserInfo(string email)
        {

            if(email!=null) NotFound();
            return Ok(_repository.getInfoByEmail(email));
        }
    }
}