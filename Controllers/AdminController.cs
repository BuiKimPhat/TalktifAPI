
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
        [Authorize]
        [HttpGet]
        [Route("GetAllUser")]
        public List<ReadUserDto> getAllUser()
        {
            return _repository.GetAllUser();
        }
    }
}