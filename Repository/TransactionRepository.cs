using Microsoft.EntityFrameworkCore;
using Splitwise.Models;
using Splitwise.SplitwiseDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SplitwiseDbContext context;
        double auid = 0, afid = 0;

        public TransactionRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Transactions> GetAllGroupTransactions()
        {
            var trans = new List<Transactions>();
            trans = context.Transactions.Where(b => b.T_ReceivedByGroup!= null).ToList();
            return trans;
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            var trans = context.Transactions.ToList();
            return trans;
        }

        public IEnumerable<Transactions> GetIndividualTransactions(int uid,int fid)
        {
            var trans = context.Transactions.Where(b=>(b.T_ReceivedByFriend==fid && b.T_PaidBy==uid)|| (b.T_ReceivedByFriend == uid && b.T_PaidBy == fid)).ToList();
            return trans;
        }

        public int AddTransaction(Transactions trans)
        {
            context.Transactions.Add(trans);
            int res = context.SaveChanges();
            return res;
        }

        public int DeleteTransaction(int id)
        {
            int res = 0;
            var trans = context.Transactions.FirstOrDefault(b => b.T_Id == id);
            if (trans != null)
            {
                trans.T_Deleted = true;
                res = context.SaveChanges();
            }
            return res;
        }

        public int UpdateTransaction(int id,Transactions tr)
        {
            int res = 0;
            var trans = context.Transactions.Find(id);
            if (trans != null)
            {
                trans.T_Id = tr.T_Id;
                trans.T_PaidBy = tr.T_PaidBy;
                trans.T_ReceivedByFriend = tr.T_ReceivedByFriend;
                trans.T_ReceivedByGroup = tr.T_ReceivedByGroup;
                trans.T_Amount = tr.T_Amount;
                res = context.SaveChanges();
            }
            return res;
        }

        public double GetSettlementIndividual(int uid, int fid)
        {
            
            double amount = 0;
            var trans = context.Transactions.Where(i=>(i.T_PaidBy==uid&&i.T_ReceivedByFriend==fid)|| (i.T_PaidBy == fid && i.T_ReceivedByFriend == uid)).ToList();
            var ug = context.GroupMembers.Where(i => i.Gm_Member == uid).Select(i=>i.GM_GroupId).ToList();
            var fg = context.GroupMembers.Where(i => i.Gm_Member == fid).Select(i=>i.GM_GroupId).ToList();
            var cg = ug.Intersect(fg).ToList();
            var setg = new List<List<GroupSettlementModel>>();

            //gets settlement of common groups
            foreach (var i in cg)           
                setg.Add(GetSettlementGroup(i).ToList());

          //  calculate auid and afid from group settlement
            foreach (var i in setg)
            {
                var u = i.Find(e => e.Gs_PayerId == uid);
                var f = i.Find(e => e.Gs_PayerId == fid);
                if ((u.Gs_Amount > 0 && f.Gs_Amount < 0) || (u.Gs_Amount < 0 && f.Gs_Amount > 0))
                {
                    if (u.Gs_Amount > f.Gs_Amount)
                    {
                        var temp = (Math.Abs(u.Gs_Amount) > Math.Abs(f.Gs_Amount)) ? Math.Abs(f.Gs_Amount) : Math.Abs(u.Gs_Amount);
                        auid = auid + temp;
                    }
                    else
                    {
                        var temp = (Math.Abs(u.Gs_Amount) <= Math.Abs(f.Gs_Amount)) ? Math.Abs(f.Gs_Amount) : Math.Abs(u.Gs_Amount);
                        afid = afid + temp;
                    }
                }
            }

            foreach (var i in trans)
            {
                if (i.T_PaidBy == uid)
                    auid =auid +(i.T_Amount);
                else
                    afid = afid+(i.T_Amount);
            }
            amount = auid - afid;

            return amount;
        }

        public IEnumerable<Transactions> GetUsersAllTransactions(int id)
        {
            var trans = context.Transactions.Where(t => t.T_PaidBy == id || t.T_ReceivedByFriend == id).ToList();
            return trans;

        }

        public IEnumerable<GroupSettlementModel> GetSettlementGroup(int id)
        {           
            var gtrans = context.Transactions.Where(b => b.T_ReceivedByGroup == id).ToList();
            var gMem = context.GroupMembers.Where(m=>m.GM_GroupId==id).ToList();
            var paidAmount=new List<double>();
            var trans = new List<GroupSettlementModel>();

            foreach (var i in gMem)
            {
                var temp=gtrans.Where(m=>m.T_PaidBy==i.Gm_Member).ToList();
                paidAmount.Add(temp.Sum(a => a.T_Amount));
            }
            var individualAmount = paidAmount.Sum()/gMem.Count();
            
            for(var i=0;i<paidAmount.Count;i++)
            {
                paidAmount[i] = paidAmount[i] - individualAmount;
                var temp = new GroupSettlementModel();
                temp.Gs_PayerId = gMem[i].Gm_Member;
                var x = context.Users.SingleOrDefault(n => n.U_Id == gMem[i].Gm_Member);
                temp.Gs_PayerName = x.U_Name;
                temp.Gs_Amount = Math.Round(paidAmount[i],2);
                trans.Add(temp);
            }            
            return trans;
        }
    }
}
