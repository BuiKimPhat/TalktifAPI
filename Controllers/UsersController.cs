using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using System.Text.Json;
using System;

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
        public ActionResult<LoginRespond> signUp(SignUpRequest user)
        {
            try{
            LoginRespond r = _repository.signUp(user);
            _repository.saveChange();
            return Ok(r);
            }catch(Exception e){
                Console.WriteLine(e.ToString());
                return NoContent();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public ActionResult<ReadUserDto> signIn(LoginRequest user)
        {
            try{
            LoginRespond r = _repository.signIn(user);
            return Ok(r);
            }catch(Exception){
                return NotFound();
            }
        }
        [HttpPost]
        [Route("{email}")]
        public ActionResult<ReadUserDto> getUserInfo(string email)
        {
            try{
            if(email!=null) NotFound();
            return Ok(_repository.getInfoByEmail(email));
            }catch(Exception){
                return NotFound();
            }
        }
        [HttpPost]
        [Route("UpdateInfo")]
        public ActionResult<ReadUserDto> updateUserInfo(UpdateInfoRequest update){
            try{
                return _repository.updateInfo(update);
            }catch(Exception){
                return NotFound();
            }
        }
        [HttpGet]
        [Route("InActiveUser")]
        public ActionResult InActiveUser (string email){
            try{
                _repository.inActiveUser(email);
                _repository.saveChange();
                return Ok();
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
    }
}