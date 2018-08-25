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
        public IEnumerable<Users> GetUsers()
        {
            var users = context.Users.Where(i=>i.U_Deleted==false).ToList();
            return users;
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

        public IEnumerable<Transactions> GetUsersTransaction(int id)
        {
            var trans = context.Transactions.Where(t=>t.T_PaidBy==id || t.T_ReceivedByFriend == id).ToList();           
            return trans;
        }

        public IEnumerable<Activity> GetUsersActivity(int id)
        {
            var act = context.Activities.Where(a => a.A_DoneBy == id || a.A_ForFriend == id);
            return act;
        }
    }
}
