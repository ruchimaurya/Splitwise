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
        Dictionary<int,string> GetFriends(int id);
        IEnumerable<FriendList> GetAllFrds();
        int AddFriend(FriendList friend);
        int InviteFriend(Invitation inv);
        int RemoveFriend(int uid, int fid);
    }
}
