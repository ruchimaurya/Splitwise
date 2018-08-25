using Splitwise.Models;
using Splitwise.SplitwiseDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class IndividualBillRepository : IIndividualBillRepository
    {
        private readonly SplitwiseDbContext context;

        public IndividualBillRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public int AddBillMember(BillMember bm)
        {
            context.BillMembers.Add(bm);
            var res = context.SaveChanges();
            return res;
        }

        public int AddIndividualBill(IndividualBillDataModel model)
        {
            var ib = new IndividualBills();
            List<Transactions> trans = new List<Transactions>();
            var bm = new List<BillMember>();
            var dt= DateTime.Now;

            ib.Ib_DateTime = dt;
            ib.Ib_Name = model.Ib_Name;
            ib.Ib_PaidBy = model.Ib_PaidBy;
            ib.Ib_Amount = model.Ib_Amount;

            bm = model.billMembers;

            context.IndividualBills.Add(ib);
            context.SaveChanges();

            ib = context.IndividualBills.OrderByDescending(d => d.Ib_Id).FirstOrDefault();
            for (int i = 0; i < bm.Count; i++)
            {
                var temp = new Transactions();
                temp.T_PaidBy = model.Ib_PaidBy;
                temp.T_Amount = model.Ib_Amount / (model.billMembers.Count + 1);
                temp.T_ReceivedByFriend = bm[i].Bm_Paidfor;
                temp.T_DateTime = dt;
                bm[i].Bm_BillId = ib.Ib_Id;
                context.BillMembers.Add(bm[i]);
                context.Transactions.Add(temp);

            }

            //add activity
            var act = new Activity();
            act.A_DoneBy = model.Ib_PaidBy;
            act.A_Description = "Added Bill " + model.Ib_Name;
            act.A_Date = dt;
            context.Activities.Add(act);

            var res = context.SaveChanges();
            return res;
        }

        public IEnumerable<IndividualBills> GetIndividualBills()
        {
            var iBills = context.IndividualBills.Where(ib=>ib.Ib_Deleted==false).ToList();
            for (int i = 0; i < iBills.Count; i++)
                iBills[i].BillMembers = context.BillMembers.Where(b => b.Bm_BillId == iBills[i].Ib_Id).ToList();
            return iBills;
        }

        public BillInformation GetIndividualBillInfo(int id)
        {
            var flist = new List<string>(new string[] { });
            var billInfo = new BillInformation();
            var bill = context.IndividualBills.SingleOrDefault(b => b.Ib_Id == id&&b.Ib_Deleted==false);
            var us = context.Users.SingleOrDefault(m => m.U_Id == bill.Ib_PaidBy);
            var mem = context.BillMembers.Where(i => i.Bm_BillId == id).ToList();
            billInfo.BI_Id = bill.Ib_Id;
            billInfo.BI_Name = bill.Ib_Name;
            billInfo.BI_PaidBy = us.U_Name;
            for (int i = 0; i < mem.Count; i++)
            {
                var temp = context.Users.SingleOrDefault(u => u.U_Id == mem[i].Bm_Paidfor);
                flist.Add(temp.U_Name);
            }
            billInfo.BI_PaidFor = flist;
            billInfo.BI_Amount = bill.Ib_Amount;
            return billInfo;
        }

       public int DeleteIndividualBill(int id)
        {
            int res = 0;
            var iBill = context.IndividualBills.FirstOrDefault(b => b.Ib_Id == id&&b.Ib_Deleted==false);
            if (iBill != null)
            {
                iBill.Ib_Deleted = true;
               // context.IndividualBills.Remove(iBill);
               // context.BillMembers.RemoveRange(context.BillMembers.Where(m => m.Bm_BillId == iBill.Ib_Id));
                var temp=context.Transactions.Where(n => n.T_DateTime == iBill.Ib_DateTime).ToList();
                foreach (var i in temp)
                    i.T_Deleted = true;
                res = context.SaveChanges();
            }
            return res;
        }
    }
}
