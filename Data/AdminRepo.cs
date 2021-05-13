using System;
using System.Collections.Generic;
using System.Linq;
using TalktifAPI.Dtos;
using TalktifAPI.Dtos.Admin;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public class AdminRepo : IAdminRepo
    {
        private readonly TalktifContext _context;

        public AdminRepo(TalktifContext context)
        {
            _context = context;
        }

        public List<GetReportRespond> GetAllReport(GetAllReportRequest request)
        {
            List<GetReportRespond> list = new List<GetReportRespond>();
            if(request.From < request.To) throw new Exception("Out of Index");
            try{
            var read = _context.Reports.OrderByDescending(p => p.Id).Take(request.To);
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
            var read = _context.Users.Where(p => p.Id != 0);
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
            if(_context.Users.FirstOrDefault(p => p.Id == id).IsAdmin==true)
                return true;
            return false;
        }

        public bool UpdateReport(UpdateReportRequest request)
        {
            try{
            var read = _context.Reports.FirstOrDefault(p => p.Id == request.Id);
            if(read!=null){
                read.Note = request.Note;
                read.Status = request.Status;
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
            var read = _context.Users.FirstOrDefault(p => p.Id == request.Id);
            if(read!=null){
                read.Name = request.Name;
                read.Email = request.Email;
                read.Gender = request.Gender;
                read.Hobbies = request.Hobbies;
                return true;
            }
            return false;
            }catch(Exception e){
                Console.Write(e.Message);
                return false;
            }
        }
    }
}