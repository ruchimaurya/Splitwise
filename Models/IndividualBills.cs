using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Splitwise.Models
{
    [Table("IndividualBills")]
    public class IndividualBills
    {
        [Key]
        public int Ib_Id { get; set; }

        [Required]
        public string Ib_Name { get; set; }

        public int Ib_PaidBy { get; set; }

        public int Ib_Amount { get; set; }

        [ForeignKey("Bm_BillId")]
        public virtual ICollection<BillMember> BillMembers { get; set; }

    }
}
