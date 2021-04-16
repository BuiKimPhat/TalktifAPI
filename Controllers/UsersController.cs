using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Http;

namespace TalktifAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]  
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
        public ActionResult<SignUpRespond> signUp(SignUpRequest user)
        {
            try{
                SignUpRespond r = _repository.signUp(user);
                _repository.saveChange();
                setTokenCookie(r.RefreshToken);
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
                setTokenCookie(r.RefreshToken);
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
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}