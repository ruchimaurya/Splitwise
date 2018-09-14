using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IIndividualBillRepository
    {
        IEnumerable<IndividualBills> GetIndividualBills();
        int AddIndividualBill(IndividualBillDataModel model);
        int AddBillMember(BillMember bm);
        BillInformation GetIndividualBillInfo(int id);
        int DeleteIndividualBill(int id);
        IEnumerable<BillInformation> GetFriendsBills(int uid, int fid);
    }
}
