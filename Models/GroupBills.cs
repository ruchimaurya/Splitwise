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
        public int Gb_Name { get; set; }

        public int Gb_PaidBy { get; set; }

        public int Gb_ForGroup { get; set; }

        public int Gb_Amount { get; set; }
    }
}
