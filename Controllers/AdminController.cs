
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Extensions.Configuration;
using TalktifAPI.Data;
using TalktifAPI.Dtos;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]  
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _repository;
        private IConfiguration _config;

        public AdminController(IAdminRepo adminrepo,IConfiguration configuration)
        {
            _repository = adminrepo;
            _config = configuration;
        }
        [AllowAnonymous]
        [HttpGet]  
        public string GetRandomToken()  
        {  
            JwtRepo jwt = new JwtRepo(_config);  
            var token = jwt.GenerateSecurityToken("fake@email.com");  
            return token;  
        }  
        [Authorize]
        [HttpGet]
        [Route("GetAllUser")]
        public List<ReadUserDto> getAllUser()
        {
            return _repository.GetAllUser();
        }
    }
}