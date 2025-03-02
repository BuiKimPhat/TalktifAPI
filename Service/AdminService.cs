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
            int count = _reportRepository.Count();
            List<GetReportRespond> list = new List<GetReportRespond>();
            var read = _reportRepository.GetAllReport(request.Top,request.OderBy,request.Filter,request.Search);         
            foreach(var a in read){
                list.Add(new GetReportRespond{
                    Id =  a.Id, Reporter =  a.Reporter, Reason =  a.Reason,
                    Suspect = a.Suspect , Status = a.Status , Note = a.Note,
                    createAt = a.CreatedAt
                });
            }
            return list;
        }

        public List<ReadUserDto> GetAllUser(GetAllUserRequest request)
        {
            int count = _userRepository.Count();
            List<ReadUserDto> list = new List<ReadUserDto>();
            List<User> a = _userRepository.GetAllUSer(request.Top,request.OderBy,request.Filter,request.Search);     
            foreach(var i in a){
                list.Add(new ReadUserDto{   
                    Email =  i.Email, Name =  i.Name, Id =  i.Id,
                    IsActive = i.IsActive, IsAdmin = i.IsAdmin, CityId = i.CityId, Gender = i.Gender 
                });
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
                read.IsActive = request.IsActive;
                read.CityId = request.CityId;
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
            _userRepository.Insert(new User(user.Name,user.Email,BC.HashPassword(user.Password),user.Gender,user.CityId,true,"ADMIN"));
            read = _userRepository.GetUserByEmail(user.Email);
            return new ReadUserDto{ Id = read.Id, Email = user.Email,IsActive = read.IsActive,
                                    Name = user.Name,IsAdmin = read.IsAdmin, 
                                    Gender= user.Gender};
        }

        public Counts GetNumOfReCord()
        {
            return new Counts{
                numOfUser = _userRepository.Count(),
                numOfChatRoom = _chatRoomRepository.Count(),
                numOfMessage = _messageRepository.Count(),
                numOfReport = _reportRepository.Count()
            };
        }

        public ReadUserDto GetUserById(int uid)
        {
            User user = _userRepository.GetById(uid);
            if(user==null) throw new Exception("user doesn't exist!");
            return new ReadUserDto{ Name = user.Name, Email= user.Email, Id = user.Id ,Gender = user.Gender,
                                    IsAdmin = user.IsAdmin, IsActive = user.IsActive, CityId = user.CityId};
        }

        public GetReportRespond GetReportById(int id)
        {
            Report report = _reportRepository.GetById(id);
            if(report==null){
                throw new Exception("Report khong ton tai");
            }else{
                return new GetReportRespond{ Id = report.Id, Reporter =  report.Reporter, Reason =  report.Reason,
                    Suspect = report.Suspect , Status = report.Status , Note = report.Note, createAt = report.CreatedAt};
            }
        }
    }
}