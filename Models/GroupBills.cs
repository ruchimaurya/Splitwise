using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("GroupBills")]
    public class GroupBills
    {
        [Key]
        public int Gb_Id { get; set; }

        [Required]
        public string Gb_Name { get; set; }

        public int Gb_PaidBy { get; set; }

        public int Gb_ForGroup { get; set; }

        public double Gb_Amount { get; set; }

        public DateTime Gb_DateTime { get; set; }

        public bool Gb_Settled { get; set; }

        public bool Gb_Deleted { get; set; }
    }
}
