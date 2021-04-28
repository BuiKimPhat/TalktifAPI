using System.Collections.Generic;
using TalktifAPI.Dtos;
using TalktifAPI.Models;

namespace TalktifAPI.Data
{
    public interface IChatRepo
    {
        bool SaveChange();
        List<Message> FecthAllMessageInChatRoom(int idchatroom);
        bool CreateChatRoom(CreateChatRoomRequest r);
        bool DeleteChatRoom(int idchatroom);
        bool ChangeNickName(ChangeNickNameRequest r);
    }
}