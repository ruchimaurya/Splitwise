using Splitwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.Repository
{
    public interface IGroupBillRepository
    {
        int AddGroupBill(GroupBills gbill);
        IEnumerable<GroupBills> GetGroupBills();
        int UpdatGroupBill(int id, GroupBills gb);
        int DeleteGroupBill(int id);
        IEnumerable<GroupBills> GetIndividualGroupBills(int id);
        BillInformation GetBillInfo(int id);
    }
}
