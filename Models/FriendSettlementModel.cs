using System.Collections.Generic;

namespace Splitwise.Models
{
    public class FriendSettlementModel
    {
        public double FS_iAmount { get; set; }

        public List<GSModel> FS_GSettle { get; set; }
    }

    public class GSModel
    {
        public int GSM_Gid { get; set; }

        public string GSM_Groupname { get; set; }

        public double GSM_Amount { get; set; }
    }

}
