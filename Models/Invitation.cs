using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitwise.Models
{
    [Table("Invitation")]
    public class Invitation
    {
        [Key]
        public int I_Id { get; set; }

        public int I_Sender { get; set; }

        [Required]
        public string I_Email { get; set; }

        public int I_Contact { get; set; }
    }
}
