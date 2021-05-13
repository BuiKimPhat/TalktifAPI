using System;
using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;
using TalktifAPI.Models;
using TalktifAPI.Repository;
using BC = BCrypt.Net.BCrypt;

namespace TalktifAPI.Service
{
    public class AdminService : IAdminService
    {
        private IReportRepository _reportRepository;
        private IUserRepository _userRepository;
        private IUserChatRoomRepository _userChatRoomRepository;
        private IUserRefreshTokenRepository _userRefreshTokenRepository;
        private IChatRoomRepository _chatRoomRepository;
        private IMessageRepository _messageRepository;

        public AdminService(IReportRepository reportRepository,IUserRepository userRepository,IUserChatRoomRepository userChatRoomRepository
                        ,IUserRefreshTokenRepository userRefreshTokenRepository,IChatRoomRepository chatRoomRepository,IMessageRepository messageRepository)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _userChatRoomRepository = userChatRoomRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _chatRoomRepository = chatRoomRepository;
            _messageRepository = messageRepository;
        }

        public bool DeleteUser(int id)
        {
            List<UserChatRoom> listUserChatRoom = _userChatRoomRepository.GetAllUserChatRoom(id);
            foreach(UserChatRoom i in listUserChatRoom){
                _userChatRoomRepository.Delete(i);
            }
            List<UserRefreshToken> listRefreshToken = _userRefreshTokenRepository.GetTokenByUID(id);
            foreach(UserRefreshToken i in listRefreshToken){
                _userRefreshTokenRepository.Delete(i);
            }
            _userRepository.Delete(_userRepository.GetById(id));
            return true;
        }
        public bool DeleteNonReferenceChatRoom(){
            List<ChatRoom> list = _chatRoomRepository.GetAllChatRoom();
            foreach(ChatRoom i in list){
                _messageRepository.RemoveMessageByChatRoomId(i.Id);
                _chatRoomRepository.Delete(i);
            }
            return true;
        }
        public List<GetReportRespond> GetAllReport(GetAllReportRequest request)
        {
            List<GetReportRespond> list = new List<GetReportRespond>();
            if(request.From < request.To) throw new Exception("Out of Index");
            try{
            var read = _reportRepository.GetAllReport(request.To,request.OderBy);
            int dem = 0;
            foreach(var u in read){
                dem++;
                if(dem < request.From) continue;
                list.Add(new GetReportRespond{
                    Id =  u.Id, Reporter =  u.Reporter, Reason =  u.Reason,
                    Suspect = u.Suspect , Status = u.Status , Note = u.Note
                });
            }
            }catch(Exception err){
                Console.Write(err.ToString());
            }
            return list;
        }

        public List<ReadUserDto> GetAllUser(GetAllUserRequest request)
        {
            List<ReadUserDto> list = new List<ReadUserDto>();
            if(request.From < request.To) throw new Exception("Out of Index");
            try{
            List<User> read = _userRepository.GetAllUSer(request.To,request.OderBy);
            int dem=0; 
            foreach(var u in read){
                dem++;
                if(dem < request.From) continue;
                list.Add(new ReadUserDto{
                    Email =  u.Email, Name =  u.Name, Id =  u.Id 
                });
            }
            }catch(Exception err){
                Console.Write(err.ToString());
            }
            return list;
        }

        public bool IsAdmin(int id)
        {
            return (bool)_userRepository.GetById(id).IsAdmin;
        }

        public bool UpdateReport(UpdateReportRequest request)
        {
            try{
            var read = _reportRepository.GetById(request.Id);
            if(read!=null){
                read.Note = request.Note;
                read.Status = request.Status;
                _reportRepository.Update(read);
                return true;
            }
            return false;
            }catch(Exception e){
                Console.Write(e.Message);
                return false;
            }
        }

        public bool UpdateUser(UpdateUserRequest request)
        {
            try{
            var read = _userRepository.GetById(request.Id);
            if(read!=null){
                read.Name = request.Name;
                read.Email = request.Email;
                read.Gender = request.Gender;
                read.Hobbies = request.Hobbies;
                _userRepository.Update(read);
                return true;
            }
            return false;
            }catch(Exception e){
                Console.Write(e.Message);
                return false;
            }
        }

        public ReadUserDto CreateUser(SignUpRequest user)
        {
            User read = _userRepository.GetUserByEmail(user.Email);
            if(read!=null) throw new Exception("User has already exist"+ read.Id);
            _userRepository.Insert(new User(user.Name,user.Email,BC.HashPassword(user.Password),user.Gender,user.Hobbies,true));
            read = _userRepository.GetUserByEmail(user.Email);
            return new ReadUserDto{ Id = read.Id, Email = user.Email,IsActive = read.IsActive,
                                    Name = user.Name,IsAdmin = read.IsAdmin, 
                                    Gender= user.Gender, Hobbies = user.Hobbies};
        }
    }
}