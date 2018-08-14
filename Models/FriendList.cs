using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Splitwise.Models
{
    [Table("FriendList")]
    public class FriendList
    {
        [Key]
        [Required]
        public int User_Id { get; set; }

        [Required]
        public int Friend_Id { get; set; }
           
    }
}
