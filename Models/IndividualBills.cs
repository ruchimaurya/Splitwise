using System;
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

        public double Ib_Amount { get; set; }

        public DateTime Ib_DateTime { get; set; }

        public bool Ib_settled { get; set; }

        public bool Ib_Deleted { get; set; }

        [ForeignKey("Bm_BillId")]
        public virtual ICollection<BillMember> BillMembers { get; set; }

    }
}
