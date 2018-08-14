using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Splitwise.Models
{
    [Table("BillMember")]
    public class BillMember
    {

        [Key]
        public int Id { get; set; }

        public int Bm_BillId { get; set; }

        public int Bm_Paidfor { get; set; }
    }
}
