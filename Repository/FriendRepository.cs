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

        public int CheckFriend(int uid, int fid)
        {
            var temp = context.FriendList.Where(f =>
            (f.User_Id == uid && f.Friend_Id == fid) ||
            (f.User_Id ==fid && f.Friend_Id == uid)).Select(i=>i.Fl_Id).ToList();
            if(temp.Count>0)
                return temp[0];
            return 0;
        }

        public IEnumerable<FriendModel> GetFriends(int id)
        {
            var friend = new List<FriendModel>();
            var friends = context.FriendList.Where(b => b.User_Id == id||b.Friend_Id==id).ToList();
            for (int i = 0; i < friends.Count; i++)
            {
                var fm= new FriendModel();
                var frd=new Users();
                if(friends[i].User_Id==id)
                  frd = context.Users.SingleOrDefault(b => b.U_Id == friends[i].Friend_Id && b.U_Deleted==false);
                else
                    frd = context.Users.SingleOrDefault(b => b.U_Id == friends[i].User_Id && b.U_Deleted==false);
                fm.Fm_Id = frd.U_Id;
                fm.Fm_Name = frd.U_Name;
                friend.Add(fm);              
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

        public FriendModel GetFriend(int fid)
        {
            var frd = new FriendModel();
            var friend = context.Users.SingleOrDefault(f=>f.U_Id==fid);
            frd.Fm_Id = friend.U_Id;
            frd.Fm_Name = friend.U_Name;
            return frd;
        }
    }
}
