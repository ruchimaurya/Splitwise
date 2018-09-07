using Microsoft.AspNetCore.Mvc;
using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IFriendRepository
    {
        IEnumerable<FriendModel> GetFriends(int id);
        IEnumerable<FriendList> GetAllFrds();
        int AddFriend(FriendList friend);
        int InviteFriend(Invitation inv);
        int RemoveFriend(int uid, int fid);
        FriendModel GetFriend(int fid);
        int CheckFriend(int uid, int fid);
    }
}
