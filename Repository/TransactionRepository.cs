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

        public TransactionRepository(SplitwiseDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<FriendTransactionModel> GetGroupsAllTransactions(int gid)
        {
            var model = new List<FriendTransactionModel>();
            var trans = new List<Transactions>();
            trans = context.Transactions.Where(b => b.T_ReceivedByGroup==gid&&b.T_ReceivedByFriend!=null).OrderByDescending(g=>g.T_Id).ToList();
            foreach(var i in trans)
            {
                var temp = new FriendTransactionModel();
                temp.FT_Id = i.T_Id;
                temp.FT_Amount = i.T_Amount;
                temp.FT_Date = i.T_DateTime;               
                    temp.FT_PaidBy = context.Users.Where(x => x.U_Id == i.T_PaidBy).Select(x => x.U_Name).ToList()[0];              
                    temp.FT_ReceivedByFriend = context.Users.Where(x => x.U_Id == i.T_ReceivedByFriend).Select(x => x.U_Name).ToList()[0];              
                    temp.FT_ReceivedByGroup = context.Groups.Where(g => g.G_Id == i.T_ReceivedByGroup).Select(g => g.G_Name).ToList()[0];
                model.Add(temp);
            }
            return model;
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            var trans = context.Transactions.ToList();
            return trans;
        }

        public IEnumerable<FriendTransactionModel> GetIndividualTransactions(int uid,int fid)
        { 
            var model = new List<FriendTransactionModel>();
            var trans = context.Transactions.Where(b=>(b.T_ReceivedByFriend==fid && b.T_PaidBy==uid)|| (b.T_ReceivedByFriend == uid && b.T_PaidBy == fid)).OrderByDescending(b=>b.T_Id).ToList();
            foreach(var i in trans)
            {
                var temp = new FriendTransactionModel();
                temp.FT_Id = i.T_Id;
                temp.FT_Amount = i.T_Amount;
                temp.FT_Date = i.T_DateTime;
                if (i.T_PaidBy == uid)
                    temp.FT_PaidBy = "You ";
                else
                    temp.FT_PaidBy = context.Users.Where(x => x.U_Id == i.T_PaidBy).Select(x => x.U_Name).ToList()[0];
                if (i.T_ReceivedByFriend == uid)
                    temp.FT_ReceivedByFriend = " you";
                else
                    temp.FT_ReceivedByFriend= context.Users.Where(x => x.U_Id == i.T_ReceivedByFriend).Select(x => x.U_Name).ToList()[0];
                if (i.T_ReceivedByGroup != null)
                    temp.FT_ReceivedByGroup = context.Groups.Where(g => g.G_Id == i.T_ReceivedByGroup).Select(g => g.G_Name).ToList()[0];
                model.Add(temp);
            }
            return model;
        }

        public int AddTransaction(Transactions trans)
        {
            var date = DateTime.Now;
            trans.T_DateTime = date;
            var act = new Activity();
            act.A_DoneBy = trans.T_PaidBy;
            act.A_ForFriend = trans.T_ReceivedByFriend;
            act.A_ForGroup = trans.T_ReceivedByGroup;
            act.A_Description = "paid $" + trans.T_Amount;
            act.A_Date = date;

            context.Transactions.Add(trans);
            context.Activities.Add(act);
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

        public FriendSettlementModel GetSettlementIndividual(int uid, int fid)
        {
            double auid = 0, afid = 0;
            var model = new FriendSettlementModel();
            var gsmList = new List<GSModel>();
            double amount = 0;
            var trans = context.Transactions.Where(i=>((i.T_PaidBy==uid&&i.T_ReceivedByFriend==fid)|| (i.T_PaidBy == fid && i.T_ReceivedByFriend == uid)) && i.T_ReceivedByGroup == null).ToList();
            var ug = context.GroupMembers.Where(i => i.Gm_Member == uid).Select(i=>i.GM_GroupId).ToList();
            var fg = context.GroupMembers.Where(i => i.Gm_Member == fid).Select(i=>i.GM_GroupId).ToList();
            var cg = ug.Intersect(fg).ToList();
            var setg = new List<List<GroupSettlementModel>>();         

            //gets settlement of common groups
            foreach (var i in cg)           
                setg.Add(GetSettlementGroup(i).ToList());
            var count = 0;
            //  calculate auid and afid from group settlement
           
            foreach (var i in setg)
            {
                var gsm = new GSModel();
                var grp = context.Groups.SingleOrDefault(k=>k.G_Id==cg[count]&&k.G_Deleted==false);
                if (grp != null)
                {
                    gsm.GSM_Gid = grp.G_Id;
                    gsm.GSM_Groupname = grp.G_Name;

                    var u = i.Find(e => e.Gs_PayerId == uid);
                    var f = i.Find(e => e.Gs_PayerId == fid);
                    if ((u.Gs_Amount > 0 && f.Gs_Amount < 0) || (u.Gs_Amount < 0 && f.Gs_Amount > 0))
                    {
                        if (u.Gs_Amount > f.Gs_Amount)
                        {
                            var temp = ((u.Gs_Amount) > (f.Gs_Amount * (-1))) ? (f.Gs_Amount * (-1)) : (u.Gs_Amount);
                            gsm.GSM_Amount = temp;
                            // auid = auid + temp;
                        }
                        else
                        {
                            var temp = ((u.Gs_Amount * (-1)) <= (f.Gs_Amount)) ? (u.Gs_Amount * (-1)) : (f.Gs_Amount);
                            gsm.GSM_Amount = temp * (-1);
                            //  afid = afid + temp;
                        }
                    }
                    gsmList.Add(gsm);
                }               
                count++;
            }
           
            foreach (var i in trans)
            {
                if (i.T_PaidBy == uid)
                    auid =auid +(i.T_Amount);
                else
                    afid = afid+(i.T_Amount);
            }
            amount = auid - afid;
            model.FS_GSettle = gsmList;
            model.FS_iAmount = amount;
            var sum = amount + model.FS_GSettle.Sum(i=>i.GSM_Amount);
            if (sum > 0)
            {
                model.FS_Receiver = context.Users.Where(i => i.U_Id == uid).Select(i => i.U_Name).ToList()[0];
                model.FS_Payer = context.Users.Where(i => i.U_Id == fid).Select(i => i.U_Name).ToList()[0];

            }
            else
            {
                model.FS_Payer = context.Users.Where(i => i.U_Id == uid).Select(i => i.U_Name).ToList()[0];
                model.FS_Receiver = context.Users.Where(i => i.U_Id == fid).Select(i => i.U_Name).ToList()[0];
            }

            return model;
        }

        public IEnumerable<Transactions> GetUsersAllTransactions(int id)
        {
            var trans = context.Transactions.Where(t => t.T_PaidBy == id || t.T_ReceivedByFriend == id).OrderByDescending(t=>t.T_Id).ToList();
            return trans;
        }

        public IEnumerable<GroupSettlementModel> GetSettlementGroup(int id)
        {           
            var gtrans = context.Transactions.Where(b => b.T_ReceivedByGroup == id&&b.T_ReceivedByFriend==null).ToList();
            var itrans= context.Transactions.Where(b => b.T_ReceivedByGroup == id && b.T_ReceivedByFriend != null).ToList();
            var gMem = context.GroupMembers.Where(m=>m.GM_GroupId==id).ToList();
            var paidAmount=new List<double>();
            var ipaidAmount =new List<double>();
            var ireceivedAmount = new List<double>();
            var trans = new List<GroupSettlementModel>();

            foreach (var i in gMem)
            {
                var temp1=gtrans.Where(m=>m.T_PaidBy==i.Gm_Member).ToList();
                paidAmount.Add(temp1.Sum(a => a.T_Amount));
            }
            var individualAmount = paidAmount.Sum()/gMem.Count();

            foreach(var i in gMem)
            {
                var temp1=itrans.Where(m => m.T_PaidBy == i.Gm_Member).ToList();
                ipaidAmount.Add(temp1.Sum(a => a.T_Amount));
                var temp2 = itrans.Where(m => m.T_ReceivedByFriend == i.Gm_Member).ToList();
                ireceivedAmount.Add(temp2.Sum(a => a.T_Amount));
            }
            
            for(var i=0;i<paidAmount.Count;i++)
            {
                paidAmount[i] = paidAmount[i]+ipaidAmount[i]-(ireceivedAmount[i] + individualAmount);
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
