using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    public class FriendTransactionModel
    {
        public int FT_Id { get; set; }

        public string FT_PaidBy { get; set; }

        public string FT_ReceivedByGroup { get; set; }

        public string FT_ReceivedByFriend { get; set; }

        public double FT_Amount { get; set; }

        public DateTime FT_Date { get; set; }
    }
}
