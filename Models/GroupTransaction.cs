using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("GroupTransaction")]
    public class GroupTransaction
    {
        [Key]
        public int Gt_Id { get; set; }

        public int Gt_PaidBy { get; set; }

        public int Gt_ReceivedBy { get; set; }

        public int Gt_Amount { get; set; }

    }
}
