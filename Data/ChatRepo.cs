using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public class ChatRepo : IChatRepo
    {
        private readonly TalktifContext _context;

        public ChatRepo(TalktifContext context)
        {
            _context = context;
        }
        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
        public bool ChangeNickName(ChangeNickNameRequest r)
        {
            UserChatRoom u = _context.UserChatRooms.FirstOrDefault(p => p.ChatRoomId == r.idchatroom);
            if(u==null) throw new Exception();
            u.NickName = r.nickname;
            return true;

        }
        public bool CreateChatRoom(CreateChatRoomRequest r)
        {
            try{
                _context.ChatRooms.Add(new ChatRoom{
                    NumOfMember = 2,
                    ChatRoomName = r.User1NickName+ " and "+r.User2NickName,
                });
                _context.SaveChanges();
                _context.UserChatRooms.Add(new UserChatRoom{
                    NickName = r.User1NickName,
                    User = r.User1Id
                });
                _context.UserChatRooms.Add(new UserChatRoom{
                    NickName = r.User2NickName,
                    User = r.User2Id
                });
                return true;
            }catch(Exception){
                return false;
            }
        }

        public bool DeleteChatRoom(int idchatroom)
        {
            try{
                ChatRoom r  = _context.ChatRooms.FirstOrDefault(r => r.Id == idchatroom); 
                int i = r.NumOfMember;
                do{
                    _context.UserChatRooms.Remove(_context.UserChatRooms.First(r => r.ChatRoomId == idchatroom));
                    _context.SaveChanges();
                    i--;
                }while(i>0);
                return true;
            }catch(Exception){
                return false;
            }

        }
        public List<Message> FecthAllMessageInChatRoom(int idchatroom)
        {
            List<Message> list = _context.Messages.Where(p => p.ChatRoomId == idchatroom).Take(100).ToList<Message>();
            if(list != null) return list;
            else throw new Exception("Khong co tin nhan nao");
        }

        public bool AddMessage(string message, int idsender, int idChatRoom)
        {
            try{
            _context.Messages.Add(new Message{
                Sender = idsender,
                ChatRoomId  = idChatRoom,
                Content = message,
                SentAt = DateTime.Now
            });
            return true;
            }catch(Exception err){
                Console.WriteLine(err.Message);
                return false;
            }
        }
    }
}