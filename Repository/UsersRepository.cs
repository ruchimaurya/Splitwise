using System;
using System.Collections.Generic;
using System.Linq;
using Splitwise.Models;
using Splitwise.SplitwiseDB;

namespace Splitwise.Repository
{
    public class UsersRepository : IUsersRepository
    {    
        private readonly SplitwiseDbContext context;

        public UsersRepository( SplitwiseDbContext context)
        {           
            this.context = context;
            
        }

        public int GetUidFromEmail(string email)
        {
            var id = context.Users.Where(u => u.U_Email == email).Select(u => u.U_Id).ToList();
            if(id.Count>0)
                return id[0];
            return 0;
        }

        public IEnumerable<Users> GetUsers()
        {
            var users = context.Users.Where(i=>i.U_Deleted==false).ToList();
            return users;
        }

        public int UserLogin(string email, string password)
        {
            var users = context.Users.Where(i => i.U_Email == email && i.U_Password == password).Select(i=>i.U_Id).ToList();
            if(users.Count>0)
                return users[0];
            return 0;
        }

        public int CreateUser(Users user)
        {
            var act = new Activity();            
            act.A_Date = DateTime.Now;
            act.A_Description = "created account";
            context.Users.Add(user);
            int res = context.SaveChanges();
            int id = context.Users.Max(p => p.U_Id);
            act.A_DoneBy = id;
            context.Activities.Add(act);
            res = context.SaveChanges();
            return res;

        }
     

        public Users GetUser(int id)
        {
            var user = context.Users.Where(b=>b.U_Deleted==false).FirstOrDefault(b => b.U_Id == id);
            return user;
        }

        public int UpdatUser(int id, Users u)
        {
            int res = 0;
            var user = context.Users.Find(id);
            var act = new Activity();
            act.A_DoneBy = user.U_Id;
            act.A_Date = DateTime.Now;
            act.A_Description = " updated profile infomation";
            if (user != null)
            {
                user.U_Name = u.U_Name;
                user.U_Email = u.U_Email;
                user.U_Password = u.U_Password;
                user.U_Contact = u.U_Contact;
                context.Activities.Add(act);
                res = context.SaveChanges();
            }
            return res;
        }

        public int DeleteUser(int id)
        {
            int res = 0;
            var user = context.Users.FirstOrDefault(b => b.U_Id == id);
            if (user != null)
            {
                user.U_Deleted = true;
            res = context.SaveChanges();
            }
            return res;
          
        }

        public IEnumerable<Groups> GetUsersGroup(int id)
        {
            var groups = new List<Groups>();
            var gm = context.GroupMembers.Where(b => b.Gm_Member == id).ToList();
            for (int i = 0; i < gm.Count; i++)
                groups.Add(context.Groups.FirstOrDefault(b => b.G_Id == gm[i].GM_GroupId));
            return groups;
        }

        public IEnumerable<FriendTransactionModel> GetUsersTransaction(int uid)
        {
            var model = new List<FriendTransactionModel>();
            var trans = context.Transactions.Where(t=>t.T_PaidBy==uid || t.T_ReceivedByFriend == uid).OrderByDescending(a => a.T_Id).ToList();
            foreach(var x in trans)
            {
                var temp = new FriendTransactionModel();
                temp.FT_Id = x.T_Id;
                temp.FT_Amount = x.T_Amount;
                temp.FT_Date = x.T_DateTime;
                if (x.T_PaidBy == uid)
                    temp.FT_PaidBy = "You ";
                else
                    temp.FT_PaidBy = context.Users.Where(r => r.U_Id == x.T_PaidBy).Select(r => r.U_Name).ToList()[0];
                if (x.T_ReceivedByFriend == uid)
                    temp.FT_ReceivedByFriend = " you";
                else if(x.T_ReceivedByFriend!=null)
                    temp.FT_ReceivedByFriend = context.Users.Where(r => r.U_Id == x.T_ReceivedByFriend).Select(r => r.U_Name).ToList()[0];
                if (x.T_ReceivedByGroup != null)
                    temp.FT_ReceivedByGroup = context.Groups.Where(g => g.G_Id == x.T_ReceivedByGroup).Select(g => g.G_Name).ToList()[0];
                model.Add(temp);
            }
            return model;
        }

        public IEnumerable<ActivityModel> GetUsersActivity(int id)
        {
            var act = context.Activities.Where(a => (a.A_DoneBy == id || a.A_ForFriend == id)&&a.A_Deleted==false).OrderByDescending(a=>a.A_Id).ToList();
            var model = new List<ActivityModel>();
            for (var i = 0; i < act.Count(); i++)
            {
                var temp = new ActivityModel();
                temp.AM_Id = act[i].A_Id;
                temp.AM_Description = act[i].A_Description;
                temp.AM_Date = act[i].A_Date;
                if (act[i].A_DoneBy == id)
                    temp.AM_DoneBy = "You";               
                else             
                    temp.AM_DoneBy = context.Users.Where(u => u.U_Id == act[i].A_DoneBy).Select(u => u.U_Name).ToList()[0];              

                if (act[i].A_ForFriend == id)
                    temp.AM_ForFriend = " you";
                else if(act[i].A_ForFriend!=null)
                    temp.AM_ForFriend = context.Users.Where(u => u.U_Id == act[i].A_ForFriend).Select(u => u.U_Name).ToList()[0];

                if (act[i].A_ForGroup!=null)
                    temp.AM_ForGroup = context.Groups.Where(u => u.G_Id == act[i].A_ForGroup).Select(u => u.G_Name).ToList()[0];

                model.Add(temp);
            }
            return model;
        }
    }
}
