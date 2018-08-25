using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface ITransactionRepository
    {
        IEnumerable<Transactions> GetAllGroupTransactions();
        IEnumerable<Transactions> GetUsersAllTransactions(int id);
        IEnumerable<Transactions> GetAllTransactions();
        IEnumerable<Transactions> GetIndividualTransactions(int uid,int fid);
        int AddTransaction(Transactions trans);
        int DeleteTransaction(int id);
        int UpdateTransaction(int id, Transactions trans);
        double GetSettlementIndividual(int uid, int fid);
        IEnumerable<GroupSettlementModel> GetSettlementGroup(int id);
    }
}
