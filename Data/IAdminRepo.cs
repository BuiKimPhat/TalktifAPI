using System.Collections.Generic;
using TalktifAPI.Dtos;

namespace TalktifAPI.Data
{
    public interface IAdminRepo
    {
        public List<ReadUserDto> GetAllUser();
    }
}