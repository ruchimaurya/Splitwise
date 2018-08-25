using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int U_Id { get; set; }

        [Required]
        public string U_Name { get; set; }

        [Required]
        public string U_Password { get; set; }

        [Required]
        public string U_Email { get; set; }

        public double U_Contact { get; set; }

        public bool U_Deleted { get; set; }

        [ForeignKey("G_Admin")]
        public virtual ICollection<Groups> Group { get; set; }

        [ForeignKey("A_ForFriend")]
        public virtual ICollection<Activity> Activity { get; set; }

        [ForeignKey("I_Sender")]
        public virtual ICollection<Invitation> Invitations { get; set; }

        [ForeignKey("A_DoneBy")]
        public virtual ICollection<Activity> Activities { get; set; }

        [ForeignKey("Bm_Paidfor")]
        public virtual ICollection<BillMember> BillMembers { get; set; }

        [ForeignKey("User_Id")]
        public virtual ICollection<FriendList> FriendListUser { get; set; }

        [ForeignKey("Friend_Id")]
        public virtual ICollection<FriendList> FriendListFriend { get; set; }

        [ForeignKey("Gb_PaidBy")]
        public virtual ICollection<GroupBills> GroupBills { get; set; }

        [ForeignKey("Gm_Member")]
        public virtual ICollection<GroupMembers> GroupMembers { get; set; }

        [ForeignKey("Gt_PaidBy")]
        public virtual ICollection<GroupTransaction> GetGroupTransactions { get; set; }

        [ForeignKey("Ib_PaidBy")]
        public virtual ICollection<IndividualBills> IndividualBills { get; set; }

        [ForeignKey("It_PaidBy")]
        public virtual ICollection<IndividualTransaction> IndividualTransactions { get; set; }
        
        [ForeignKey("T_ReceivedByFriend")]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
