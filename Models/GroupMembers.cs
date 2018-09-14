using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("GroupMember")]
    public class GroupMembers
    {
        [Key]
        public int GM_Id { get; set; }

        public int GM_GroupId { get; set; }

        public int Gm_Member { get; set; }

        public bool Gm_Deleted { get; set; }
    }
}
