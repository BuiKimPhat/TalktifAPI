using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _repository;

        public UsersController(IUserRepo repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("~/api/Users/SignUp")]
        public ActionResult<ReadUserDto> signUp(CreateUserDto user)
        {
            if(!_repository.isUserExists(user.Email)) NotFound();
            CreateUserDto u = _repository.signUp(user);
            _repository.saveChange();
            return Ok(getUserInfo(u.Email));
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