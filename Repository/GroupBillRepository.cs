using Splitwise.Models;
using Splitwise.SplitwiseDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class GroupBillRepository : IGroupBillRepository
    {

        private readonly SplitwiseDbContext context;

        public GroupBillRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public int AddGroupBill(GroupBills gbill)
        {
            var date = DateTime.Now;

            ////add activity
            var act = new Activity();
            act.A_DoneBy = gbill.Gb_PaidBy;
            act.A_ForGroup = gbill.Gb_ForGroup;
            act.A_Description = "Added new bill of " + gbill.Gb_Amount + "$ as " + gbill.Gb_Name;
            act.A_Date = date;
            context.Activities.Add(act);

            var trans = new Transactions();
            trans.T_PaidBy = gbill.Gb_PaidBy;
            trans.T_ReceivedByGroup = gbill.Gb_ForGroup;
            trans.T_Amount = gbill.Gb_Amount;
            trans.T_DateTime = date;
            gbill.Gb_DateTime = date;

            context.GroupBills.Add(gbill);
           context.Transactions.Add(trans);

            int res = context.SaveChanges();
            return res;
        }

        public int DeleteGroupBill(int id)
        {
            int res = 0;
            var gb = context.GroupBills.FirstOrDefault(b => b.Gb_Id == id);
            if (gb != null)
            {
                //add activity
                var act = new Activity();
                act.A_DoneBy = gb.Gb_PaidBy;
                act.A_ForGroup = gb.Gb_ForGroup;
                act.A_Description = "Deleted "+ gb.Gb_Name;
                act.A_Date = DateTime.Now;
                context.Activities.Add(act);

                gb.Gb_Deleted = true;
              //  context.Transactions.Remove(context.Transactions.SingleOrDefault(t => t.T_DateTime == gb.Gb_DateTime));
                res = context.SaveChanges();
            }
            return res;
        }

        public BillInformation GetBillInfo(int id)
        {
            var billInfo = new BillInformation();
            var bill = context.GroupBills.SingleOrDefault(b => b.Gb_Id == id);
            var bm = context.Users.SingleOrDefault(m => m.U_Id == bill.Gb_PaidBy);
            var grp = context.Groups.SingleOrDefault(i => i.G_Id == bill.Gb_ForGroup);
            billInfo.BI_Id = bill.Gb_Id;
            billInfo.BI_Name = bill.Gb_Name;
            billInfo.BI_PaidBy = bm.U_Name;
            billInfo.BI_PaidFor = new List<string>(new string[] { grp.G_Name });
            billInfo.BI_Date = bill.Gb_DateTime;
            billInfo.BI_Amount = bill.Gb_Amount;
            return billInfo;
        }

        public IEnumerable<GroupBills> GetGroupBills()
        {
            var gBills = context.GroupBills.ToList();
            return gBills;
        }

        public IEnumerable<GroupBills> GetIndividualGroupBills(int id)
        {
            var gBill = context.GroupBills.Where(b => b.Gb_ForGroup == id).ToList();
            return gBill;
        }

        public int UpdatGroupBill(int id, GroupBills gb)
        {
            int res = 0;
            var gBill = context.GroupBills.Find(id);
            if (gBill != null)
            {
                //add activity
                var act = new Activity();
                act.A_DoneBy = gBill.Gb_PaidBy;
                act.A_ForGroup = gBill.Gb_ForGroup;
                act.A_Description = "Updated " + gb.Gb_Name;
                act.A_Date = DateTime.Now;
                context.Activities.Add(act);

                gBill.Gb_Name = gb.Gb_Name;
                gBill.Gb_PaidBy = gb.Gb_PaidBy;
                gBill.Gb_ForGroup = gb.Gb_ForGroup;
                gBill.Gb_Amount = gb.Gb_Amount;
                res = context.SaveChanges();
            }
            return res;
        }
    }
}
