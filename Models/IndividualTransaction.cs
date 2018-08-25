using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("IndividualTransaction")]
    public class IndividualTransaction
    {
        [Key]
        public int It_Id { get; set; }

        public int It_BillId { get; set; }

        public int It_PaidBy { get; set; }

        public double It_Amount { get; set; }

        public bool It_Delete { get; set; }
    }
}
