using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<FriendTransactionModel> GetGroupsAllTransactions(int gid);
        IEnumerable<Transactions> GetUsersAllTransactions(int id);
        IEnumerable<Transactions> GetAllTransactions();
        IEnumerable<FriendTransactionModel> GetIndividualTransactions(int uid,int fid);
        int AddTransaction(Transactions trans);
        int DeleteTransaction(int id);
        int UpdateTransaction(int id, Transactions trans);
        FriendSettlementModel GetSettlementIndividual(int uid, int fid);      
        IEnumerable<GroupSettlementModel> GetSettlementGroup(int id);
    }
}
