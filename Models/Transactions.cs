using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("Transaction")]
    public class Transactions
    {

        [Key]
        public int T_Id { get; set; }

        public int T_PaidBy { get; set; }

        public int? T_ReceivedByGroup { get; set; }

        public int? T_ReceivedByFriend { get; set; }

        public double T_Amount { get; set; }

        public DateTime T_DateTime { get; set; }

        public bool T_Deleted { get; set; }
    }
}
