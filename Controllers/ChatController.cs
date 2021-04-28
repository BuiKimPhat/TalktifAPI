using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TalktifAPI.Data;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepo _repository;

        public ChatController(IChatRepo repository)
        {
            _repository = repository;
        }
        [HttpPost]   
        [Authorize]     
        [Route("CreateChatRoom")]
        public ActionResult Create(CreateChatRoomRequest createChatRoom)
        {
            try{
                bool check =_repository.CreateChatRoom(createChatRoom);
                _repository.SaveChange();
                if(check==true) return Ok();
                else return NoContent();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return NoContent();
            }
        }
        [HttpPost]   
        [Authorize]     
        [Route("{userid}")]
        public ActionResult<List<Message>> FetchAllChatRoom(int userid)
        {
            try{
                List<Message> list =_repository.FecthAllMessageInChatRoom(userid);
                if(list!=null) return Ok(list);
                else return BadRequest();
            }catch(Exception e){
                Console.WriteLine(e.ToString()+"createchatroom err");
                return BadRequest();
            }
        }
    }
}