using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Splitwise.Models
{

    [Table("Groups")]
    public class Groups
    {
        [Key]
        public int G_Id { get; set; }

        [Required]
        public string G_Name { get; set; }

        public int G_Admin { get; set; }

        public DateTime G_Date{ get; set; }

        [ForeignKey("GM_GroupId")]
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }

        [ForeignKey("Gb_ForGroup")]
        public virtual ICollection<GroupBills> GroupBills { get; set; }

        [ForeignKey("Gt_ReceivedBy")]
        public virtual ICollection<GroupTransaction> GroupTransactions { get; set; }

    }
}
