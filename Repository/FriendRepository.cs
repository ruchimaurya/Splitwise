using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Splitwise.Models;
using Splitwise.SplitwiseDB;

namespace Splitwise.Repository
{
    
    public class FriendRepository : IFriendRepository
    {
        private readonly SplitwiseDbContext context;

        public FriendRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public int AddFriend(FriendList friend)
        {
            var act = new Activity();
            act.A_DoneBy = friend.User_Id;
            act.A_ForFriend = friend.Friend_Id;
            act.A_Description = "became friend with";
            act.A_Date = DateTime.Now;
            context.Activities.Add(act);
            context.FriendList.Add(friend);
            int res = context.SaveChanges();
            return res;

        }

        public IEnumerable<FriendList> GetAllFrds()
        {
            var friends = context.FriendList.ToList();
            return friends;
        }

        public Dictionary<int, string> GetFriends(int id)
        {
            var friend = new Dictionary<int ,string>();
            var friends = context.FriendList.Where(b => b.User_Id == id||b.Friend_Id==id).ToList();
            for (int i = 0; i < friends.Count; i++)
            {
                var frd=new Users();
                if(friends[i].User_Id==id)
                  frd = context.Users.SingleOrDefault(b => b.U_Id == friends[i].Friend_Id && b.U_Deleted==false);
                else
                    frd = context.Users.SingleOrDefault(b => b.U_Id == friends[i].User_Id && b.U_Deleted==false);
                friend.Add(frd.U_Id,frd.U_Name);              
            }
              
            return friend;
        }

        public int InviteFriend(Invitation inv)
        {
            var act = new Activity();
            act.A_DoneBy = inv.I_Sender;           
            act.A_Description = "sent invitation to "+inv.I_Email;
            act.A_Date = DateTime.Now;
            context.Activities.Add(act);

            context.Invitation.Add(inv);
            int res = context.SaveChanges();
            return res;
        }

        public int RemoveFriend(int uid, int fid)
        {
            int res = 0;
            var rf = context.FriendList.FirstOrDefault(b =>( b.User_Id == uid && b.Friend_Id == fid)|| ( b.User_Id == fid && b.Friend_Id == uid));
            if (rf != null)
            {
                context.FriendList.Remove(rf);
                res = context.SaveChanges();
            }
            return res;
        }
    }
}
