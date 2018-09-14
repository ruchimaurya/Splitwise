using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.Models;
using Splitwise.SplitwiseDB;

namespace Splitwise.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SplitwiseDbContext context;

        public GroupRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public int AddMemberToGroup(GroupMembers gm)
        {            
            context.GroupMembers.Add(gm);
            int res = context.SaveChanges();
            return res;
        }

        public int CreateGroup(Groups group)
        {
            var admin = new GroupMembers();
            admin.Gm_Member = group.G_Admin;
            var date = DateTime.Now;
            group.G_Date = date;
            context.Groups.Add(group);
            context.SaveChanges();
            var gid = context.Groups.Where(g => g.G_Admin == group.G_Admin && g.G_Date == group.G_Date)
                .Select(g=>g.G_Id).ToList();
            admin.GM_GroupId = gid[0];
            context.GroupMembers.Add(admin);

            //add activity
            var act = new Activity();
            act.A_DoneBy = group.G_Admin;
            act.A_ForGroup = gid[0];
            act.A_Description = "Created Group " + group.G_Name;
            act.A_Date = date;
            context.Activities.Add(act);
            
            int res = context.SaveChanges();
            return gid[0];
        }

        public int DeleteGroup(int id)
        {
            int res = 0;
            var group = context.Groups.FirstOrDefault(b => b.G_Id == id);
            if (group != null)
            {
                //add activity
                var act = new Activity();
                act.A_DoneBy = group.G_Admin;
                act.A_ForGroup = id;
                act.A_Description = "Deleted Group " + group.G_Name;
                act.A_Date = DateTime.Now;
                context.Activities.Add(act);

                group.G_Deleted = true;
                res = context.SaveChanges();
            }
            return res;

        }

        public Groups GetGroup(int id)
        {
            var group = context.Groups.FirstOrDefault(b => b.G_Id == id);
            return group;
        }

        public IEnumerable<GroupMembersModel> GetGroupMembers(int id)
        {
            var members = new List<GroupMembersModel>();
            var gm = context.GroupMembers.Where(b => b.GM_GroupId == id).Select(b=>b.Gm_Member).ToList();
            
            foreach(var m in gm)
            {
                var mem = new GroupMembersModel();
                var temp = context.Users.FirstOrDefault(b => b.U_Id == m);
                mem.M_MemberId = temp.U_Id;
                mem.M_Name = temp.U_Name;
                mem.M_Email = temp.U_Email;
                members.Add(mem);
            }
            //for (int i = 0; i < gm.Count; i++)
            //    members.Insert(0, context.Users.FirstOrDefault(b => b.U_Id == gm[i].Gm_Member));
            return members;
        }

        public IEnumerable<Groups> GetGroups()
        {
            var groups = context.Groups.Where(g=>g.G_Deleted==false).ToList();
            return groups;
        }

        public IEnumerable<Groups> GetUsersGroups(int id)
        {
            var group = context.Groups.Where(b => b.G_Id == id).ToList();
            return group;
        }

        public int RemoveMemberFromGroup(int gid, int mid)
        {
            int res = 0;
            var gm = context.GroupMembers.FirstOrDefault(b => b.GM_GroupId == gid && b.Gm_Member==mid);
            if (gm != null)
            {
                gm.Gm_Deleted = true;
                context.GroupMembers.Update(gm);
                res = context.SaveChanges();
            }
            return res;
        }

        public int UpdatGroup(int id, Groups g)
        {
            int res = 0;
            var group = context.Groups.Find(id);
            if (group != null)
            {
                //add activity
                var act = new Activity();
                act.A_DoneBy = g.G_Admin;
                act.A_ForGroup = g.G_Id;
                act.A_Description = "Updated GRoup " + group.G_Name;
                act.A_Date = DateTime.Now;
                context.Activities.Add(act);

                group.G_Id = g.G_Id;
                group.G_Name = g.G_Name;
                group.G_Admin = g.G_Admin;
                group.G_Date = g.G_Date;
                res = context.SaveChanges();
            }
            return res;
        }

        public GroupInformation GetGroupInformation(int id)
        {
            var GInfo = new GroupInformation();

            var grp = context.Groups.SingleOrDefault(g => g.G_Id == id);
            GInfo.Gi_GroupId = grp.G_Id;
            GInfo.Gi_Name = grp.G_Name;
            GInfo.Gi_Date = grp.G_Date;
            var tempAdm = context.Users.Where(i => i.U_Id == grp.G_Admin).Select(i => i.U_Name).ToList();
            GInfo.Gi_Admin = tempAdm[0];                
            var gm=context.GroupMembers.Where(g => g.GM_GroupId == id&&g.Gm_Deleted==false).Select(g => g.Gm_Member).ToList();
                      
            var gmem = context.GroupMembers.Where(m => m.GM_GroupId == id).Select(m=>m.Gm_Member).ToList();
            var memList = new List<string>();
            foreach(var i in gmem){
                var mem = context.Users.Where(u => u.U_Id == i).Select(m => m.U_Name).ToList();
                memList.Add(mem[0]);
            }
            GInfo.Gi_Members = memList;

            var gb = context.GroupBills.Where(b=>b.Gb_ForGroup==id).OrderByDescending(b=>b.Gb_Id).Select(b=>b.Gb_Id).ToList();
            var billList = new List<BillInformation>();
            foreach (var b in gb)
            {
                var x = new GroupBillRepository(context);
                var temp= x.GetBillInfo(b);
                billList.Add(temp);
            }
            GInfo.Gi_Bills = billList;

            return GInfo;
        }
    }
}
